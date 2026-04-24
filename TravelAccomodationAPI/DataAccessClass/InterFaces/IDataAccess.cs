using Dapper;
using System.Data;

namespace TravelAccomodationAPI.DataAccessClass.InterFaces
{
    public interface IDataAccess
    {

        Task<T> GetAsync<T>(string sp, DynamicParameters parameters = null);

        Task<IEnumerable<T>> GetListAsync<T>(
            string sp, DynamicParameters parameters = null);

        Task<int> ExecuteAsync(
            string sp, DynamicParameters parameters = null, IDbTransaction transaction = null);

        Task<T> ExecuteScalarAsync<T>(
            string sp, DynamicParameters parameters = null, IDbTransaction transaction = null);

        public  Task BulkInsertAsync<T>(
                string storedProcedure,
                string tvpParameterName,
                string tvpTypeName,
                IEnumerable<T> data);

    }
}
