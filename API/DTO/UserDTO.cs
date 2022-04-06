using Models;

namespace API.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }

        public static UserDTO FillObject(User user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Lastname = user.Lastname,
            };
        }
    }
}
