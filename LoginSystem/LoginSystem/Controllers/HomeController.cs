using ApiAuthentication.Interfaces;
using ApiAuthentication.Model;
using LoginSystem.Enum;
using LoginSystem.Models;
using LoginSystem.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Reflection;

namespace LoginSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService _authService;
        private readonly IUtil _util;
        private readonly IMemoryCache _cache;

        public HomeController(ILogger<HomeController> logger, IAuthService authService, IUtil util, IMemoryCache cache)
        {
            _logger = logger;
            _authService = authService;
            _util = util;
            _cache = cache;
        }

        public IActionResult Index(LoginModel model)
        {
            var cacheKey = "Login";
            var authentication = new AuthResponse();

            if (!_cache.TryGetValue<AuthResponse>(cacheKey, out authentication))
            {
                var request = new AuthRequest();

                if (model.Email == null)
                {
                    return View(model);
                }

                request.Email = model.Email;
                request.Password = _util.EncryptPassword(model.Password);
                authentication = _authService.GetLoginUser(request);

                if (authentication.Token != null)
                {
                    model.Name = authentication.Name;

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                    _cache.Set(cacheKey, authentication, cacheOptions);
                    return RedirectToAction("Index", "Dashboard", model);
                }

                ViewBag.Alert = _util.ShowAlert(Alerts.Danger, "Wrong username or password, please try again!");
                return View(model);
            }
            return RedirectToAction("Index", "Dashboard", model);
        }
    }
}