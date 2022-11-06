using Models.Request;
using Models.Response;

namespace Interfaces
{
    public interface IUser
    {
        public Task<Response> AddUser(UserRequest userRequest);
        public Task<Response> GetUser(int userId);
        public Task<Response> UpdateUser(int userId, UserUpdateRequest userUpdateRequest);
        public Task<Response> BlockUser(int userId);
        public Task<Response> UnblockUser(int userId);
        public Response LoginUser(string email, string password);
    }
}
