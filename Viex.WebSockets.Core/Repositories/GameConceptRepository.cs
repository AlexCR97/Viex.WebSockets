using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Viex.WebSockets.Core.Models;

namespace Viex.WebSockets.Core.Repositories
{
    public class GameConceptRepository : IRepository<GameConcept>
    {
        private readonly ViexContext _context;

        public GameConceptRepository(ViexContext context)
        {
            _context = context;
        }

        public Task<int> Add(GameConcept model)
        {
            throw new NotImplementedException();
        }

        public Task Edit(int id, GameConcept model)
        {
            throw new NotImplementedException();
        }

        public async Task<GameConcept> GetFirst(Expression<Func<GameConcept, bool>> predicate)
        {
            return await _context.GameConcepts.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<GameConcept>> GetWhere(Expression<Func<GameConcept, bool>> predicate)
        {
            return await _context.GameConcepts
                .Where(predicate)
                .ToListAsync();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
