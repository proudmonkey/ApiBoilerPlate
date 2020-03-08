using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Data.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiBoilerPlate.Data.DataManager
{
    public class PersonManager : DbFactoryBase, IPersonManager
    {
        private readonly ILogger<PersonManager> _logger;
        public PersonManager(IConfiguration config, ILogger<PersonManager> logger) : base(config)
        {
            _logger = logger;
        }

        public async Task<(IEnumerable<Person> Persons, Pagination Pagination)> GetPersonsAsync(UrlQueryParameters urlQueryParameters)
        {
            IEnumerable<Person> persons;
            int recordCount = default;

            ////For PosgreSql
            //var query = @"SELECT ID, FirstName, LastName, DateOfBirth FROM Person
            //                ORDER BY ID DESC 
            //                Limit @Limit Offset @Offset";


            ////For SqlServer
            var query = @"SELECT ID, FirstName, LastName, DateOfBirth FROM Person
                            ORDER BY ID DESC
                            OFFSET @Limit * (@Offset -1) ROWS
                            FETCH NEXT @Limit ROWS ONLY";

            var param = new DynamicParameters();
            param.Add("Limit", urlQueryParameters.PageSize);
            param.Add("Offset", urlQueryParameters.PageNumber);

            if (urlQueryParameters.IncludeCount)
            {
                query += " SELECT COUNT(ID) FROM Person";
                var pagedRows = await DbQueryMultipleAsync<Person, int>(query, param);

                persons = pagedRows.Data;
                recordCount = pagedRows.RecordCount;
            }
            else
            {
                persons = await DbQueryAsync<Person>(query, param);
            }

            var metadata = new Pagination
            {
                PageNumber = urlQueryParameters.PageNumber,
                PageSize = urlQueryParameters.PageSize,
                TotalRecords = recordCount

            };

            return (persons, metadata);

        }
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await DbQueryAsync<Person>("SELECT * FROM Person");
        }

        public async Task<Person> GetByIdAsync(object id)
        {
            return await DbQuerySingleAsync<Person>("SELECT * FROM Person WHERE ID = @ID", new { id });
        }

        public async Task<long> CreateAsync(Person person)
        {
            string sqlQuery = $@"INSERT INTO Person (FirstName, LastName, DateOfBirth) 
                                     VALUES (@FirstName, @LastName, @DateOfBirth)
                                     SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return await DbQuerySingleAsync<long>(sqlQuery, person);
        }
        public async Task<bool> UpdateAsync(Person person)
        {
            string sqlQuery = $@"IF EXISTS (SELECT 1 FROM Person WHERE ID = @ID) 
                                            UPDATE Person SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth
                                            WHERE ID = @ID";

            return await DbExecuteAsync<bool>(sqlQuery, person);
        }
        public async Task<bool> DeleteAsync(object id)
        {
            string sqlQuery = $@"IF EXISTS (SELECT 1 FROM Person WHERE ID = @ID)
                                        DELETE Person WHERE ID = @ID";

            return await DbExecuteAsync<bool>(sqlQuery, new { id });
        }
        public async Task<bool> ExistAsync(object id)
        {
            return await DbExecuteScalarAsync("SELECT COUNT(1) FROM Person WHERE ID = @ID", new { id });
        }

        public async Task<bool> ExecuteWithTransactionScope()
        {

            using (var dbCon = new SqlConnection(DbConnectionString))
            {
                await dbCon.OpenAsync();
                var transaction = await dbCon.BeginTransactionAsync();

                try
                {
                    //Do stuff here Insert, Update or Delete
                    Task q1 = dbCon.ExecuteAsync("<Your SQL Query here>");
                    Task q2 = dbCon.ExecuteAsync("<Your SQL Query here>");
                    Task q3 = dbCon.ExecuteAsync("<Your SQL Query here>");

                    await Task.WhenAll(q1, q2, q3);

                    //Commit the Transaction when all query are executed successfully

                    await transaction.CommitAsync();
                }
                catch(Exception ex)
                {
                    //Rollback the Transaction when any query fails
                    transaction.Rollback();
                    _logger.Log(LogLevel.Error, ex, "Error when trying to execute database operations within a scope.");

                    return false;
                }
            }
            return true;
        }

    }
}
