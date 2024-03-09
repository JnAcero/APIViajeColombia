using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViajeColombia.Models;

namespace ViajeColombia.DataAccess.Repositories.Contracts
{
    public interface IGenericRepository<T>
    {
        Task<IQueryable<T>> GetAll();
        Task<IQueryable<T>> GetWhen(Expression<Func<T, bool>>? filter);
        Task<List<T>> CreateRange(List<T> models);
    }
}
