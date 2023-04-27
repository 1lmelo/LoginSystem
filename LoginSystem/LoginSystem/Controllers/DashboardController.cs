using LoginSystem.Enum;
using LoginSystem.Models;
using LoginSystem.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ApiAuthentication.Services;
using Microsoft.AspNetCore.Authentication;
using ApiAuthentication.Interfaces;
using ApiAuthentication.Model;
using System.Net;

namespace LoginSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserDAL _userDAL;
        private readonly IUtil _util;
        private readonly IEmailDAL _emailDAL;
        private readonly IAuthService _authService;
        private const string passwordRecovery = "Troca123*";

        public DashboardController(ILogger<HomeController> logger, IUserDAL userDAL, IUtil util, IEmailDAL emailDAL, IAuthService authService)
        {
            _logger = logger;
            _userDAL = userDAL;
            _util = util;
            _emailDAL = emailDAL;
            _authService = authService;
        }
        public IActionResult Index(LoginModel model)
        {
            return View(model);
        }

        public IActionResult Register(UserModel user)
        {
            if (user.Password != null)
            {
                user.Password = _util.EncryptPassword(user.Password);
                var createUser = _userDAL.Insert(user);
                if (createUser > 0)
                {
                    ViewBag.Alert = _util.ShowAlert(Alerts.Success, "User created successfully!");
                    _ = _emailDAL.EmailCreateUser(user.Email, user.Name).GetAwaiter();
                }
                else
                {
                    ViewBag.Alert = _util.ShowAlert(Alerts.Danger, "Error creating user!");
                }
            }
            return View();
        }

        public IActionResult Recovery(UserModel user)
        {
            if (user.Email != null)
            {
                user.Password = _util.EncryptPassword(passwordRecovery);
                var recoveryUser = _userDAL.Update(user);
                if (recoveryUser.Count > 0)
                {
                    ViewBag.Alert = _util.ShowAlert(Alerts.Success, "The new password has been sent to the email!");
                    _ = _emailDAL.RecoveryUser(user.Email).GetAwaiter();
                }
                else
                {
                    ViewBag.Alert = _util.ShowAlert(Alerts.Danger, "No user registered with this e-mail!");
                }
            }
                return View();
        }
    }
}
