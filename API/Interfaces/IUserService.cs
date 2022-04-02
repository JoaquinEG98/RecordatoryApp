using API.Request;

namespace API.Interfaces
{
    public interface IUserService
    {
        public Task<Response.Response> AddUser(UserRequest userRequest);
    }
}
