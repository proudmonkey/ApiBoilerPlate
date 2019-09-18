using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoreBoilerPlate.Data
{
    public abstract class DBFactoryBase
    {
        private readonly IConfiguration _config;
        public DBFactoryBase(IConfiguration config)
        {
            _config = config;
        }

        internal IDbConnection DbConnection
        {
            get {
                return new SqlConnection(_config.GetConnectionString("SQLDBConnectionString"));
            }
        }

        public virtual async Task<IEnumerable<T>> DbQueryAsync<T>(string sql, object parameters = null)
        {
            using (IDbConnection dbCon = DbConnection)
            {
                dbCon.Open();
                if (parameters == null)
                    return await dbCon.QueryAsync<T>(sql);
    
                return await dbCon.QueryAsync<T>(sql, parameters);
            }
        }
        public virtual async Task<T> DbQuerySingleAsync<T>(string sql, object parameters)
        {
            using (IDbConnection dbCon = DbConnection)
            {
                dbCon.Open();
                return await dbCon.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }

        public virtual async Task<bool> DbExecuteAsync<T>(string sql, object parameters)
        {
            using (IDbConnection dbCon = DbConnection)
            {
                dbCon.Open();
                return await dbCon.ExecuteAsync(sql, parameters) > 0;
            }
        }

        public virtual async Task<bool> DbExecuteScalarAsync(string sql, object parameters)
        {
            using (IDbConnection dbCon = DbConnection)
            {
                dbCon.Open();
                return await dbCon.ExecuteScalarAsync<bool>(sql, parameters);
            }
        }
    }
}
