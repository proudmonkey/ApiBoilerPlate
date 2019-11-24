using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Data.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiBoilerPlate.Data.DataManager
{
    public class PersonManager : DbFactoryBase, IPersonManager
    {
        public PersonManager(IConfiguration config) : base(config)
        {

        }

        public async Task<(IEnumerable<Person> Persons, Pagination Pagination)> GetPersonsAsync(UrlQueryParameters urlQueryParameters)
        {
            IEnumerable<Person> persons;
            int recordCount = 0;

            ////For PosgreSql
            //var query = @"SELECT ID, FirstName, LastName FROM Person
            //                ORDER BY ID DESC 
            //                Limit @Limit Offset @Offset";


            ////For SqlServer
            var query = @"SELECT ID, FirstName, LastName FROM Person
                            ORDER BY ID DESC
                            OFFSET @Limit * (@Offset -1) ROWS
                            FETCH NEXT @Limit ROWS ONLY";

            var param = new DynamicParameters();
            param.Add("Limit", urlQueryParameters.PageSize);
            param.Add("Offset", urlQueryParameters.PageNumber);

            if (urlQueryParameters.IncludeCount)
            {
                query += " SELECT COUNT(ID) FROM Person";
                var pagedRows = await DbQueryMultipleAsync<Person>(query, param);

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
    }
}
