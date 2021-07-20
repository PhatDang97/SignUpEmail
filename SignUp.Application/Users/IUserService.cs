using SignUp.Application.Users.Dtos;
using SignUp.Core.Models;
using System.Threading.Tasks;

namespace SignUp.Application.Users
{
    public interface IUserService
    {
        Task<bool> Register(RegisterRequest request);
        Task<string> CofirmationCode(User user);
        Task<User> GetUserByUserName(string userName);
        Task<bool> SignIn(User user, string password);
        Task<User> GetUserByUserId(int userId);
        Task<bool> ConfirmEmail(User user, string code);
    }
}
