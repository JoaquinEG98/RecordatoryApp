using API.Interfaces;
using API.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

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

        #region GET Methods
        [HttpGet]
        [Route("api/{controller}/getuser/{userId}")]
        [Authorize]
        public async Task<Response.Response> GetUser([FromRoute] int userId)
        {
            Response.Response response = await _userService.GetUser(userId);
            return response;
        }

        [HttpGet]
        [Route("api/{controller}/login/{email}/{password}")]
        public Response.Response Login([FromRoute] string email, string password)
        {
            Response.Response response = _userService.LoginUser(email, password);
            return response;
        }
        #endregion

        #region PUT Methods
        [HttpPut]
        [Route("api/{controller}/updateuser/{userId}")]
        [Authorize]
        public async Task<Response.Response> UpdateUser([FromRoute] int userId, [FromBody] UserUpdateRequest userUpdateRequest)
        {
            Response.Response response = await _userService.UpdateUser(userId, userUpdateRequest);
            return response;
        }

        [HttpPut]
        [Route("api/{controller}/blockuser/{userId}")]
        [Authorize]
        public async Task<Response.Response> BlockUser([FromRoute] int userId)
        {
            Response.Response response = await _userService.BlockUser(userId);
            return response;
        }

        [HttpPut]
        [Route("api/{controller}/unblockuser/{userId}")]
        [Authorize]
        public async Task<Response.Response> UnblockUser([FromRoute] int userId)
        {
            Response.Response response = await _userService.UnblockUser(userId);
            return response;
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
