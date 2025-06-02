namespace PixCharge.API.Models
{
    public class User
    {
        public User(string name, string email, string passwordHash, string? pixKey, 
            string role = "Client")
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            PixKey = pixKey;
            CreatedAt = DateTime.UtcNow;
            Role = role;
        }

        protected User() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string? PixKey { get; private set; }
        public string Role { get; set; } = "Client"; 
        public DateTime CreatedAt { get; private set; }

        public ICollection<Client> Clients { get; private set; } = new List<Client>();
        public ICollection<Pix> Charges { get; private set; } = new List<Pix>();
    }
}