namespace PixCharge.API.Models
{
    public class PixCharge
    {
        public PixCharge(Guid userId, Guid clientId, decimal amount, string description, string qrCodeUrl, string pixCopyPaste, string externalChargeId, DateTime expirationDate)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ClientId = clientId;
            Amount = amount;
            Description = description;
            QrCodeUrl = qrCodeUrl;
            PixCopyPaste = pixCopyPaste;
            ExternalChargeId = externalChargeId;
            ExpirationDate = expirationDate;
            Status = "Pending";
            CreatedAt = DateTime.UtcNow;
        }
        protected PixCharge() { }


        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }
        public User? User { get; private set; }

        public Guid ClientId { get; private set; }
        public Client? Client { get; private set; }

        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public string QrCodeUrl { get; private set; }
        public string PixCopyPaste { get; private set; }
        public string ExternalChargeId { get; private set; }
        public string Status { get; private set; } // Pending, Paid, Canceled, Expired
        public DateTime? PaymentDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public void MarkAsPaid(DateTime paymentDate)
        {
            Status = "Paid";
            PaymentDate = paymentDate;
        }

        public void Cancel()
        {
            Status = "Canceled";
        }
    }

}