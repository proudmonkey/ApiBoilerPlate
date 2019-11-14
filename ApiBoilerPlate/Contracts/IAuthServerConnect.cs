using System.Threading.Tasks;

namespace ApiBoilerPlate.Contracts
{
    public interface IAuthServerConnect
    {
        Task<(string AccessToken, string ErrorMessage)> RequestAccessToken();
    }
}
