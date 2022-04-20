using BlogsApi.Dtos;
using System.Threading.Tasks;

namespace BlogsApi.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(UserDTOS userDTOS);
        Task<string> CreateToken();
    }
}
