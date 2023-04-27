using ApiAuthentication.Interfaces;
using ApiAuthentication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthService _authService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public AuthResponse Auth([FromBody] AuthRequest request)
        {
            try
            {
                var verifyLogin = _authService.GetLoginUser(request);
                return verifyLogin;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}




