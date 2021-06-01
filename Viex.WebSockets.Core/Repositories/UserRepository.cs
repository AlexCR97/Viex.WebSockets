using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Viex.WebSockets.Core.Errors;
using Viex.WebSockets.Core.Models;

namespace Viex.WebSockets.Core.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly MyContext _context;

        public UserRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<int> Add(User model)
        {
            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.UserId;
        }

        public Task Edit(int id, User model)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetFirst(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.FirstOrDefaultAsync(predicate);
        }

        public Task<IEnumerable<User>> GetWhere(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserId == id);

            if (user == null)
                throw new UserNotFoundException(id);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }
    }
}
