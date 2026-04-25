using Dapper;
using System.Data;

namespace TravelAccomodationAPI.DataAccessClass.InterFaces
{
    public interface IDataAccess
    {

        Task<T> GetAsync<T>(string sp, DynamicParameters parameters = null, IDbTransaction transaction = null);

        Task<IEnumerable<T>> GetListAsync<T>(
            string sp, DynamicParameters parameters = null, IDbTransaction transaction = null);

        Task<int> ExecuteAsync(
            string sp, DynamicParameters parameters = null, IDbTransaction transaction = null);

        Task<T> ExecuteScalarAsync<T>(
            string sp, DynamicParameters parameters = null, IDbTransaction transaction = null);

        public Task<T> ExecuteWithResponseAsync<T>(string sp, DynamicParameters? parameters, IDbTransaction? transaction = null);

    }
}
