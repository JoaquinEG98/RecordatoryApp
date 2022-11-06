namespace Models.Request
{
    public class UserUpdateRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public bool Confirmed { get; set; }
        public bool Blocked { get; set; }
    }
}
