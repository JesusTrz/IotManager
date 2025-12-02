namespace IotManager.Models
{
    public class RegisterVM
    {
        public required string Email { get; set; }

        public required string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Password { get; set; }
    }
}
