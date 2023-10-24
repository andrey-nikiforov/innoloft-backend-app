using Innoloft_Application.Dto;
using Innoloft_Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Innoloft_Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInfoController : Controller
    {

        private readonly UserService _userService;

        public UserInfoController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<UserInfoDto> GetUserInfo()
        {
          return await _userService.GetUserInfo();
        }
    }
}
