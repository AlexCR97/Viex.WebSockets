using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Viex.WebSockets.Core.Errors;
using Viex.WebSockets.Core.Models;

namespace Viex.WebSockets.Core.Repositories
{
    public class GameRoomRepository : IRepository<GameRoom>
    {
        private readonly ViexContext _context;

        public GameRoomRepository(ViexContext context)
        {
            _context = context;
        }

        public async Task<int> Add(GameRoom model)
        {
            await _context.GameRooms.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.GameRoomId;
        }

        public async Task Edit(int id, GameRoom model)
        {
            var snapshot = await _context.GameRooms.FirstOrDefaultAsync(room => room.GameRoomId == id);

            if (snapshot == null)
                throw new GameRoomNotFoundException(id);

            snapshot.CurrentRound = model.CurrentRound;
            snapshot.CurrentRoundGameConcepts = model.CurrentRoundGameConcepts;
            snapshot.CurrentRoundLetter = model.CurrentRoundLetter;
            snapshot.CurrentRoundRemainingSeconds = model.CurrentRoundRemainingSeconds;
            snapshot.Players = model.Players;
            snapshot.TotalRounds = model.TotalRounds;

            _context.GameRooms.Update(snapshot);
            await _context.SaveChangesAsync();
        }

        public async Task<GameRoom> GetFirst(Expression<Func<GameRoom, bool>> predicate)
        {
            return await _context.GameRooms
                .Include(room => room.Host)
                .Include(room => room.Players)
                .Include(room => room.CurrentRoundGameConcepts)
                .FirstOrDefaultAsync(predicate);
        }

        public Task<IEnumerable<GameRoom>> GetWhere(Expression<Func<GameRoom, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            var snapshot = await _context.GameRooms.FirstOrDefaultAsync(room => room.GameRoomId == id);

            if (snapshot == null)
                throw new GameRoomNotFoundException(id);

            _context.GameRooms.Remove(snapshot);
            await _context.SaveChangesAsync();
        }
    }
}
