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
    public class WaitingRoomRepository : IRepository<WaitingRoom>
    {
        private readonly MyContext _context;

        public WaitingRoomRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<int> Add(WaitingRoom model)
        {
            await _context.WaitingRooms.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.WaitingRoomId;
        }

        public async Task Edit(int id, WaitingRoom model)
        {
            var snapshot = await _context.WaitingRooms
                .Include(a => a.Host)
                .Include(a => a.Guests)
                .FirstOrDefaultAsync(a => a.WaitingRoomId == id);

            if (snapshot == null)
                throw new WaitingRoomNotFoundException(id);

            snapshot.Guests = model.Guests;
            snapshot.HostId = model.HostId;
            snapshot.Password = model.Password;
            snapshot.RemainingSeconds = model.RemainingSeconds;

            _context.WaitingRooms.Update(snapshot);
            await _context.SaveChangesAsync();
        }

        public async Task<WaitingRoom> GetFirst(Expression<Func<WaitingRoom, bool>> predicate)
        {
            return await _context.WaitingRooms
                .Include(a => a.Host)
                .Include(a => a.Guests)
                .FirstOrDefaultAsync(predicate);
        }

        public Task<IEnumerable<WaitingRoom>> GetWhere(Expression<Func<WaitingRoom, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            var room = await _context.WaitingRooms.FirstOrDefaultAsync(a => a.WaitingRoomId == id);

            if (room == null)
                throw new WaitingRoomNotFoundException(id);

            _context.WaitingRooms.Remove(room);

            await _context.SaveChangesAsync();
        }
    }
}
