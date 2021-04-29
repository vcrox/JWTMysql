using JWTMysql.Models;
using System.Threading.Tasks;

namespace JWTMysql.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> Register(string email, string password);
        Task<ServiceResponse<ResponseLogin>> LoginAsync(string email, string password);
    }
}
