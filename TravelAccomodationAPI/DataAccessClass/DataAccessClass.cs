using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using TravelAccomodationAPI.DataAccessClass.InterFaces;
using TravelAccomodationAPI.Shared.CommonMethods;
using TravelAccomodationAPI.Shared.DBHelper;

namespace TravelAccomodationAPI.DataAccessClass
{
    public class DataAccessClass : IDataAccess
    {
        private readonly DbContext _context;

        public DataAccessClass(DbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetAsync<T>(string sp, DynamicParameters? parameters)
    {
        using var connection = _context.GetConnection();
       

        return await connection.QueryFirstOrDefaultAsync<T>(
            sp,
            parameters,
            commandType: CommandType.StoredProcedure,
            commandTimeout: 30);
    }

    public async Task<IEnumerable<T>> GetListAsync<T>(string sp, DynamicParameters? parameters)
    {
        using var connection = _context.GetConnection();
       

        var result = await connection.QueryAsync<T>(
            sp,
            parameters,
            commandType: CommandType.StoredProcedure,
            commandTimeout: 30);

        return result.ToList();
    }

    public async Task<int> ExecuteAsync(string sp, DynamicParameters? parameters,  IDbTransaction transaction = null)
    {
        using var connection = _context.GetConnection();
       

        return await connection.ExecuteAsync(
            sp,
            parameters,
            transaction,
            commandType: CommandType.StoredProcedure,
            commandTimeout: 30);
    }

    public async Task<T> ExecuteScalarAsync<T>(string sp, DynamicParameters? parameters ,IDbTransaction transaction = null)
            {
                using var connection = _context.GetConnection();
       

                return await connection.ExecuteScalarAsync<T>(
                    sp,
                    parameters,
                    transaction,
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: 30);
            }

        public async Task BulkInsertAsync<T>(
                string storedProcedure,
                string tvpParameterName,
                string tvpTypeName,
                IEnumerable<T> data)
        {
            var dt = FileUploadCommon.ToDataTable(data);
            using var connection = _context.GetConnection();


            var parameters = new DynamicParameters();
            parameters.Add(tvpParameterName, dt.AsTableValuedParameter(tvpTypeName));

            await connection.ExecuteAsync(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

    }

}
