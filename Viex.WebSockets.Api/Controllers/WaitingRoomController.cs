using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viex.WebSockets.Core.Errors;
using Viex.WebSockets.Core.Models;
using Viex.WebSockets.Core.Payloads;
using Viex.WebSockets.Core.Repositories;

namespace Viex.WebSockets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaitingRoomController : ControllerBase
    {
        private readonly UserRepository _users;
        private readonly WaitingRoomRepository _waitingRooms;

        public WaitingRoomController(UserRepository users, WaitingRoomRepository waitingRooms)
        {
            _users = users;
            _waitingRooms = waitingRooms;
        }

        [HttpPost("create")]
        public async Task<ActionResult<WaitingRoom>> Create([FromBody] CreateWaitingRoomPayload payload)
        {
            try
            {
                await ThrowIfUsernameIsTaken(payload.Username);
                await ThrowIfWaitingRoomIsTaken(payload.RoomPassword);

                var userId = await _users.Add(new User
                {
                    Username = payload.Username,
                    IsHost = true,
                });

                var waitingRoomId = await _waitingRooms.Add(new WaitingRoom
                {
                    HostId = userId,
                    Password = payload.RoomPassword,
                    RemainingSeconds = 60,
                });

                var createdWaitingRoom = await _waitingRooms.GetFirst(a => a.WaitingRoomId == waitingRoomId);

                return Ok(createdWaitingRoom);
            }
            catch (UsernameTakenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (WaitingRoomTakenException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("password/{password}")]
        public async Task<ActionResult<WaitingRoom>> GetByPassword(string password)
        {
            try
            {
                await ThrowIfWaitingRoomNotFound(password);
                var room = await _waitingRooms.GetFirst(a => a.Password == password);
                return Ok(room);
            }
            catch (WaitingRoomNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("join")]
        public async Task<ActionResult<WaitingRoom>> Join([FromBody] JoinWaitingRoomPayload payload)
        {
            try
            {
                await ThrowIfUsernameIsTaken(payload.Username);
                await ThrowIfWaitingRoomNotFound(payload.RoomPassword);
                await ThrowIfUserAlreadyJoinedWaitingRoom(payload.Username, payload.RoomPassword);

                var userId = await _users.Add(new User
                {
                    Username = payload.Username,
                });

                var createdUser = await _users.GetFirst(a => a.UserId == userId);
                var waitingRoomToJoin = await _waitingRooms.GetFirst(a => a.Password == payload.RoomPassword);

                waitingRoomToJoin.Guests.Add(createdUser);
                await _waitingRooms.Edit(waitingRoomToJoin.WaitingRoomId, waitingRoomToJoin);

                return Ok(waitingRoomToJoin);
            }
            catch (UsernameTakenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (WaitingRoomNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (GuestAlreadyInWaitingRoomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task ThrowIfUserAlreadyJoinedWaitingRoom(string username, string roomPassword)
        {
            var waitingRoom = await _waitingRooms.GetFirst(w => w.Password == roomPassword);
            bool hasUser = waitingRoom.Guests.Any(user => user.Username == username);

            if (hasUser)
                throw new GuestAlreadyInWaitingRoomException(username, roomPassword);
        }

        private async Task ThrowIfUsernameIsTaken(string username)
        {
            var user = await _users.GetFirst(u => u.Username == username);

            if (user != null)
                throw new UsernameTakenException(username);
        }

        private async Task ThrowIfWaitingRoomNotFound(string roomPassword)
        {
            var waitingRoom = await _waitingRooms.GetFirst(w => w.Password == roomPassword);

            if (waitingRoom == null)
                throw new WaitingRoomNotFoundException(roomPassword);
        }

        private async Task ThrowIfWaitingRoomIsTaken(string roomPassword)
        {
            var waitingRoom = await _waitingRooms.GetFirst(w => w.Password == roomPassword);

            if (waitingRoom != null)
                throw new WaitingRoomTakenException(roomPassword);
        }
    }
}
