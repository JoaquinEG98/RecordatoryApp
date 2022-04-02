using API.Interfaces;
using API.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class UserController
    {
        #region Dependency inversion
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region POST Methods
        [HttpPost]
        [Route("api/{controller}/adduser")]
        public async Task<Response.Response> AddUser([FromBody] UserRequest userRequest)
        {
            Response.Response response = await _userService.AddUser(userRequest);
            return response;
        }
        #endregion
    }
}
