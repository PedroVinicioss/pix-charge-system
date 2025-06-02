namespace PixCharge.API.Models.InputModels
{
    public class CreateUserInputModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string PixKey { get; set; }
        public string Role { get; set; }

        public CreateUserInputModel(string email, string password, string name, string pixKey, string role = "Client")
        {
            Email = email;
            Password = password;
            Name = name;
            PixKey = pixKey;
            Role = role;
        }

        public User toEntity()
        {
            return new User(Name, Email, Password, PixKey);
        }
    }
}