using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Data;
using ApiBoilerPlate.Domain.Entity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiBoilerPlate.Domain
{
    public class PersonManager : DBFactoryBase, IPersonManager
    {
        public PersonManager(IConfiguration config) : base(config)
        {

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
            var sqlQuery = $@"INSERT INTO Person (FirstName, LastName, DateOfBirth) 
                                     VALUES (@FirstName, @LastName, @DateOfBirth)
                                     SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return await DbQuerySingleAsync<long>(sqlQuery, person);
        }
        public async Task<bool> UpdateAsync(Person person)
        {
            var sqlQuery = $@"IF EXISTS (SELECT 1 FROM Person WHERE ID = @ID) 
                                            UPDATE Person SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth
                                            WHERE ID = @ID";

            return await DbExecuteAsync<bool>(sqlQuery, person);
        }
        public async Task<bool> DeleteAsync(object id)
        {
            var sqlQuery = $@"IF EXISTS (SELECT 1 FROM Person WHERE ID = @ID)
                                        DELETE Person WHERE ID = @ID";

            return await DbExecuteAsync<bool>(sqlQuery, new { id });
        }
        public async Task<bool> ExistAsync(object id)
        {
            return await DbExecuteScalarAsync("SELECT COUNT(1) FROM Person WHERE ID = @ID", new { id });
        }
    }
}
