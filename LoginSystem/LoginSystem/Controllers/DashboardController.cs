using LoginSystem.Enum;
using LoginSystem.Models;
using LoginSystem.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserDAL _userDAL;
        private readonly IUtil _util;
        private readonly IEmailDAL _emailDAL;

        public DashboardController(ILogger<HomeController> logger, IUserDAL userDAL, IUtil util, IEmailDAL emailDAL)
        {
            _logger = logger;
            _userDAL = userDAL;
            _util = util;
            _emailDAL = emailDAL;
        }
        public IActionResult Index()
        {
            return View();
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

        public IActionResult Recovery()
        {
            return View();
        }
    }
}
