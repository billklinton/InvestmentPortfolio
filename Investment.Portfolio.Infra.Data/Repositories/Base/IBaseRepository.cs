using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Investment.Portfolio.Infra.Data.Repositories.Base
{
    public interface IBaseRepository
    {
        Task ExecuteAsync(string sql, object parameters);
        Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object parameters);
        Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object parameters);
        IEnumerable<TResult> Query<TResult>(string sql, object parameters);
    }
}
