using Helpers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    public class User
    {
        #region Dependency injection
        private readonly IUnitOfWork _unitOfWork;
        private readonly Encryption _encryption;

        public User(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            _encryption = new Encryption();
        }
        #endregion

        #region Methods
        public async Task<Models.User> Add(Models.User user)
        {
            ValidateUser(user);

            user.Password = _encryption.Hash(user.Password!);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();

            Logger logger = new Logger()
            {
                Description = "Se registró",
                LogDate = DateTime.Now,
                User = user
            };
            LogSingleton.GetInstance().Add(_unitOfWork, logger);
            await _unitOfWork.SaveAsync();

            return user;
        }

        public async Task<Models.User> Update(Models.User user)
        {
            ValidateUser(user);

            _unitOfWork.Users.Update(user);

            Logger logger = new Logger()
            {
                Description = "Editó su usuario",
                LogDate = DateTime.Now,
                User = user
            };
            LogSingleton.GetInstance().Add(_unitOfWork, logger);

            await _unitOfWork.SaveAsync();

            return user;
        }

        public async Task<Models.User> Get(int userId)
        {
            return await _unitOfWork.Users.GetAsync(userId);
        }

        public Models.User Login(string email, string password)
        {
            Models.User user = _unitOfWork.Users.GetAll().Where(x => x.Email == email).FirstOrDefault()!;

            password = _encryption.Hash(password);
            
            ValidateLoginUser(user, password);

            return user;
        }
        #endregion

        #region Tools
        private bool ValidateEmail(string email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void ValidateUser(Models.User user)
        {
            // Validations

            if (string.IsNullOrWhiteSpace(user.Email) || user.Name!.Length < 2) throw new Exception("El email no puede estar vacio ni tener menos de 2 caracteres");
            if (ValidateEmail(user.Email) == false) throw new Exception("El email no tiene el formato correcto.");
            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 8) throw new Exception("La contraseña no puede estar vacia ni tener menos de 8 caracteres");
            if (string.IsNullOrWhiteSpace(user.Name) || user.Name.Length < 2) throw new Exception("El nombre no puede estar vacio ni tener menos de 2 caracteres");
            if (string.IsNullOrWhiteSpace(user.Lastname) || user.Lastname.Length < 2) throw new Exception("El apellido no puede estar vacio ni tener menos de 2 caracteres");

            // End of validations
        }

        private void ValidateLoginUser(Models.User user, string password)
        {
            if (user == null) throw new Exception("Ese usuario no existe.");
            if (user.Blocked == true) throw new Exception("Ese usuario está bloqueado.");
            if (user.Confirmed == false) throw new Exception("Ese usuario no está confirmado.");
            if (user.Password != password) throw new Exception("La contraseña es incorrecta.");
        }
        #endregion
    }
}
