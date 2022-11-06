using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Models.Response;

namespace API.Controllers
{
    [ApiController]
    public class UserController
    {
        #region Dependency inversion
        private readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }
        #endregion

        #region GET Methods
        [HttpGet]
        [Route("api/{controller}/getuser/{userId}")]
        [Authorize]
        public async Task<Response> GetUser([FromRoute] int userId)
        {
            Response response = await _userService.GetUser(userId);
            return response;
        }

        [HttpGet]
        [Route("api/{controller}/login/{email}/{password}")]
        public Response Login([FromRoute] string email, string password)
        {
            Response response = _userService.LoginUser(email, password);
            return response;
        }
        #endregion

        #region PUT Methods
        [HttpPut]
        [Route("api/{controller}/updateuser/{userId}")]
        [Authorize]
        public async Task<Response> UpdateUser([FromRoute] int userId, [FromBody] UserUpdateRequest userUpdateRequest)
        {
            Response response = await _userService.UpdateUser(userId, userUpdateRequest);
            return response;
        }

        [HttpPut]
        [Route("api/{controller}/blockuser/{userId}")]
        [Authorize]
        public async Task<Response> BlockUser([FromRoute] int userId)
        {
            Response response = await _userService.BlockUser(userId);
            return response;
        }

        [HttpPut]
        [Route("api/{controller}/unblockuser/{userId}")]
        [Authorize]
        public async Task<Response> UnblockUser([FromRoute] int userId)
        {
            Response response = await _userService.UnblockUser(userId);
            return response;
        }
        #endregion

        #region POST Methods
        [HttpPost]
        [Route("api/{controller}/adduser")]
        public async Task<Response> AddUser([FromBody] UserRequest userRequest)
        {
            Response response = await _userService.AddUser(userRequest);
            return response;
        }
        #endregion
    }
}
