using System.Threading.Tasks;

namespace ApiBoilerPlate.Contracts
{
    public interface IApiConnect
    {
        Task<T> PostDataAsync<T,T2>(string endPoint, T2 dto);
        Task<T> GetDataAsync<T>(string endPoint);
    }
}
