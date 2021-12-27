using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Investment.Portfolio.Infra.Data.Repositories.Base
{
    public class BaseRepository : IBaseRepository
    {
        private IDbConnection _connection;

        public BaseRepository(string connectionString)
        {
            _connection ??= new MySqlConnection(connectionString);
            _connection.Open();
        }

        public async Task ExecuteAsync(string sql, object parameters)
            => await _connection.ExecuteAsync(sql, parameters);

        public IEnumerable<TResult> Query<TResult>(string sql, object parameters)
            => _connection.Query<TResult>(sql, parameters);

        public async Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object parameters)
            => await _connection.QueryAsync<TResult>(sql, parameters);

        public Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object parameters)
            => _connection.QueryFirstOrDefaultAsync<TResult>(sql, parameters);
    }
}
