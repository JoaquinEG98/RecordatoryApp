using Models;

namespace API.DTO
{
    public class UserDTO
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }

        public static UserDTO FillObject(User user)
        {
            return new UserDTO()
            {
                Email = user.Email,
                Name = user.Name,
                Lastname = user.Lastname,
            };
        }
    }
}
