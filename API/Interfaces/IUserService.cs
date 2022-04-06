using API.Request;
using Models;

namespace API.Interfaces
{
    public interface IUserService
    {
        public Task<Response.Response> AddUser(UserRequest userRequest);
        public Task<Response.Response> GetUser(int userId);
        public Task<Response.Response> UpdateUser(int userId, UserUpdateRequest userUpdateRequest);
        public Task<Response.Response> BlockUser(int userId);
        public Task<Response.Response> UnblockUser(int userId);
        public Response.Response LoginUser(string email, string password);
    }
}
