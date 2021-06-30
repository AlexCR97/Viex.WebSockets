using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viex.WebSockets.Core.Errors;
using Viex.WebSockets.Core.Extensions;
using Viex.WebSockets.Core.Models;
using Viex.WebSockets.Core.Repositories;
using Viex.WebSockets.Core.Utils;

namespace Viex.WebSockets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameRoomController : ControllerBase
    {
        private readonly GameConceptRepository _gameConcepts;
        private readonly GameRoomRepository _gameRooms;
        private readonly UserRepository _users;
        private readonly WaitingRoomRepository _waitingRooms;

        public GameRoomController(GameConceptRepository gameConcepts, GameRoomRepository gameRooms, UserRepository users, WaitingRoomRepository waitingRooms)
        {
            _gameConcepts = gameConcepts;
            _gameRooms = gameRooms;
            _users = users;
            _waitingRooms = waitingRooms;
        }

        [HttpGet("password/{password}")]
        public async Task<ActionResult<GameRoom>> GetByPassword(string password)
        {
            try
            {
                await ThrowIfGameRoomNotFound(password);
                var room = await _gameRooms.GetFirst(a => a.Password == password);
                return Ok(room);
            }
            catch (GameRoomNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("startGame/{waitingRoomPassword}")]
        public async Task<ActionResult<GameRoom>> StartGame(string waitingRoomPassword)
        {
            try
            {
                await ThrowIfWaitingRoomNotFound(waitingRoomPassword);

                var waitingRoom = await _waitingRooms.GetFirst(a => a.Password == waitingRoomPassword);
                
                var createdGameRoomId = await _gameRooms.Add(new GameRoom
                {
                    Players = GetPlayers(waitingRoom.Host, waitingRoom.Guests),
                    HostId = waitingRoom.HostId,
                    Password = waitingRoomPassword,
                    TotalRounds = 5,
                });

                var gameRoom = await _gameRooms.GetFirst(a => a.GameRoomId == createdGameRoomId);

                return Ok(gameRoom);
            }
            catch (WaitingRoomNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("startRound/{gameRoomPassword}")]
        public async Task<ActionResult<GameRoom>> StartRound(string gameRoomPassword)
        {
            try
            {
                await ThrowIfGameRoomNotFound(gameRoomPassword);
                await ThrowIfGameRoundsFinished(gameRoomPassword);

                var gameRoom = await _gameRooms.GetFirst(a => a.Password == gameRoomPassword);
                
                gameRoom.CurrentRound += 1;
                gameRoom.CurrentRoundGameConcepts = await GetRandomGameConcepts();
                gameRoom.CurrentRoundLetter = RandomUtils.UpperCaseLetter();
                gameRoom.CurrentRoundRemainingSeconds = 30;

                await _gameRooms.Edit(gameRoom.GameRoomId, gameRoom);

                return Ok(gameRoom);
            }
            catch (GameRoomNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (GameRoundsFinishedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task ThrowIfGameRoomNotFound(string roomPassword)
        {
            var gameRoom = await _gameRooms.GetFirst(a => a.Password == roomPassword);

            if (gameRoom == null)
                throw new GameRoomNotFoundException(roomPassword);
        }

        private async Task ThrowIfGameRoundsFinished(string roomPassword)
        {
            var gameRoom = await _gameRooms.GetFirst(a => a.Password == roomPassword);

            if (gameRoom.CurrentRound >= gameRoom.TotalRounds)
                throw new GameRoundsFinishedException();
        }

        private async Task ThrowIfWaitingRoomNotFound(string roomPassword)
        {
            var waitingRoom = await _waitingRooms.GetFirst(a => a.Password == roomPassword);

            if (waitingRoom == null)
                throw new WaitingRoomNotFoundException(roomPassword);
        }

        private IList<User> GetPlayers(User host, IList<User> guests)
        {
            return guests
                .Append(host)
                .ToList()
                .Shuffle();
        }

        private async Task<IList<GameConcept>> GetRandomGameConcepts()
        {
            var gameConcepts = await _gameConcepts.GetWhere(_ => true);

            return gameConcepts
                .ToList()
                .Shuffle()
                .Take(5)
                .ToList();
        }
    }
}
