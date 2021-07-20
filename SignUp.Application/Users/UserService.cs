using Microsoft.AspNetCore.Identity;
using SignUp.Application.Users.Dtos;
using SignUp.Core.Models;
using System.Threading.Tasks;

namespace SignUp.Application.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var userName = await _userManager.FindByNameAsync(request.UserName);
            if (userName != null)
                return false;

            var email = await _userManager.FindByEmailAsync(request.Email);
            if (email != null)
                return false;

            var user = new User()
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dob = request.Dob
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<string> CofirmationCode(User user)
        {
            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<User> GetUserByUserId(int userId)
        {
            var user = await _userManager.FindByNameAsync(userId.ToString());
            return user;
        }

        public async Task<bool> ConfirmEmail(User user, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return true;
            return false;
        }

        public async Task<bool> SignIn(User user, string password)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (signInResult.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}