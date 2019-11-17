using ApiBoilerPlate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiBoilerPlate.Contracts
{
    public interface IApiConnect<T> where T : class
    {
        Task<T> CreateSampleData(PersonDTO dto);
        Task<T> GetSampleData(long id);
    }
}
