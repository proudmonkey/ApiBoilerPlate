using ApiBoilerPlate.Data;
using ApiBoilerPlate.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiBoilerPlate.Contracts
{
    public interface IPersonManager : IRepository<Person>
    {
        Task<IEnumerable<Person>> GetPersonsAsync(UrlQueryParameters urlQueryParameters);
        
        //Add more class specific methods here when neccessary
    }
}
