namespace PixCharge.API.Models
{
    public class Notification
    {
        public Notification(Guid userId, Guid clientId, string type, string message, Guid? pixChargeId = null)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ClientId = clientId;
            PixChargeId = pixChargeId;
            Type = type;
            Message = message;
            Status = "Pending";
            CreatedAt = DateTime.UtcNow;
        }
        protected Notification() { }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid? PixChargeId { get; private set; }

        public string Type { get; private set; } // Email, WhatsApp, etc.
        public string Status { get; private set; } // Pending, Sent, Failed
        public string Message { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public void MarkAsSent() => Status = "Sent";
        public void MarkAsFailed() => Status = "Failed";
    }

}