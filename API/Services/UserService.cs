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
        private readonly TokenService _tokenService;

        public UserService(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _userService = new Library.User(unitOfWork);
            _tokenService = tokenService;
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

        public async Task<Response.Response> UpdateUser(int userId, UserUpdateRequest userUpdateRequest)
        {
            try
            {
                User userGet = await _userService.Get(userId);

                userGet.Email = userUpdateRequest.Email;
                userGet.Password = userUpdateRequest.Password;
                userGet.Name = userUpdateRequest.Name;
                userGet.Lastname = userUpdateRequest.Lastname;
                userGet.Confirmed = userUpdateRequest.Confirmed;
                userGet.Blocked = userUpdateRequest.Blocked;

                User userUpdate = _userService.Update(userGet);
                return Response.Response.FillObject(UserDTO.FillObject(userUpdate), System.Net.HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Response.Response.FillObject(null!, System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response.Response> BlockUser(int userId)
        {
            try
            {
                User userGet = await _userService.Get(userId);

                userGet.Blocked = true;

                User userUpdate = _userService.Update(userGet);
                return Response.Response.FillObject(UserDTO.FillObject(userUpdate), System.Net.HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Response.Response.FillObject(null!, System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response.Response> UnblockUser(int userId)
        {
            try
            {
                User userGet = await _userService.Get(userId);

                userGet.Blocked = false;

                User userUpdate = _userService.Update(userGet);
                return Response.Response.FillObject(UserDTO.FillObject(userUpdate), System.Net.HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Response.Response.FillObject(null!, System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public Response.Response LoginUser(string email, string password)
        {
            try
            {
                User userLogin = _userService.Login(email, password);                
                string token = _tokenService.GenerateJWT(userLogin);

                return Response.Response.FillObject(LoginDTO.FillObject(userLogin, token), System.Net.HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Response.Response.FillObject(null!, System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
