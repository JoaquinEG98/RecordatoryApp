using API.DTO;
using API.Interfaces;
using API.Request;
using Helpers;
using Models;

namespace API.Services
{
    public class UserService : IUserService
    {
        #region Dependency injection
        private readonly Library.User _userService;

        public UserService(IUnitOfWork unitOfWork)
        {
            _userService = new Library.User(unitOfWork);
        }
        #endregion

        #region Methods
        public async Task<Response.Response> AddUser(UserRequest userRequest)
        {
            try
            {
                if (userRequest == null) throw new Exception("El usuario que se recibió es nulo.");

                User user = new User()
                {
                    Email = userRequest.Email,
                    Password = userRequest.Password,
                    Name = userRequest.Name,
                    Lastname = userRequest.Lastname,
                    Blocked = false,
                    Confirmed = true
                };

                await _userService.Add(user);

                return Response.Response.FillObject(null!, System.Net.HttpStatusCode.OK, "Usuario creado con éxito!");
            }
            catch (Exception ex)
            {
                return Response.Response.FillObject(null!, System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response.Response> GetUser(int userId)
        {
            try
            {
                User user = await _userService.Get(userId);

                return Response.Response.FillObject(UserDTO.FillObject(user), System.Net.HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Response.Response.FillObject(null!, System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
