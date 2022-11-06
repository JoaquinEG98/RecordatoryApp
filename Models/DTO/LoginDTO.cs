namespace Models.DTO
{
    public class LoginDTO
    {
        public string? Token { get; set; }
        public UserDTO? User { get; set; }

        public static LoginDTO FillObject(User user, string token)
        {
            return new LoginDTO()
            {
                User = UserDTO.FillObject(user),
                Token = token
            };
        }
    }
}
