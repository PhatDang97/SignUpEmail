using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using SignUp.Application.Users;
using SignUp.Application.Users.Dtos;
using System.Threading.Tasks;

namespace SignUp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public LoginController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var user = await _userService.GetUserByUserName(username);

            if(user != null)
            {
                // sign in
                var signInResult = await _userService.SignIn(user, password);
                if(signInResult == true)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SignUp(RegisterRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Register(request);
                if(result == true)
                {
                    var user = await _userService.GetUserByUserName(request.UserName);
                    //Generate of the email token
                    string code = await _userService.CofirmationCode(user);

                    var callBackUrl = Url.Action("VerifyEmail", "Login", new { userId = user.Id, code = code }, Request.Scheme, Request.Host.ToString());
                     
                    await _emailService.SendAsync("dangthanhphat23031997@gmail.com", "email verify", callBackUrl);

                    RedirectToAction("EmailVerification");
                }
            }

            return View();
        }

        public async Task<IActionResult> VerifyEmail(int userId, string code)
        {
            var user = await _userService.GetUserByUserId(userId);
            if (user == null)
                return BadRequest();

            var result = await _userService.ConfirmEmail(user, code);
            if(result == true)
            {
                return View();
            }

            return BadRequest();
        }

        public IActionResult EmailVerification()
        {
            return View();
        }
    }
}
