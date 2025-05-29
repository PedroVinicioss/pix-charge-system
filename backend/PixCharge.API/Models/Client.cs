namespace PixCharge.API.Models
{
    public class Client
    {
        public Client(Guid userId, string name, string email, string phone, string document)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
            CreatedAt = DateTime.UtcNow;
        }

        protected Client() { }
        
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public User? User { get; private set; }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Document { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ICollection<Pix> Charges { get; private set; } = new List<Pix>();
    }

}