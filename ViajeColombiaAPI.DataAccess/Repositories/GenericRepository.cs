using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViajeColombia.DataAccess.DBContext;
using ViajeColombia.DataAccess.Repositories.Contracts;
using ViajeColombia.Models;

namespace ViajeColombia.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ViajeColombiaContext _context;

        public GenericRepository(ViajeColombiaContext context)
        {
            _context = context;
        }

        public async Task<List<T>> CreateRange(List<T> models)
        {
            await _context.Set<T>().AddRangeAsync(models);
            await _context.SaveChangesAsync();
            return models;
        }

        public async Task<IQueryable<T>> GetAll()
        {
            var query = _context.Set<T>();
            return query;
        }

        public async Task<IQueryable<T>> GetWhen(Expression<Func<T, bool>>? filter)
        {
            var query = filter is null ? _context.Set<T>() : _context.Set<T>().Where(filter);

            return query;
        }
    }
}
