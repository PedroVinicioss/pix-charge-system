namespace PixCharge.API.Models
{
    public class PixRecurringCharge
    {
        public PixRecurringCharge(Guid userId, Guid clientId, decimal amount, string description, string frequency, DateTime nextChargeDate)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ClientId = clientId;
            Amount = amount;
            Description = description;
            Frequency = frequency;
            NextChargeDate = nextChargeDate;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
        protected PixRecurringCharge() { }


        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }
        public User? User { get; private set; }

        public Guid ClientId { get; private set; }
        public Client? Client { get; private set; }

        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public string Frequency { get; private set; } // Monthly, Weekly, etc.
        public DateTime NextChargeDate { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public void Cancel()
        {
            IsActive = false;
        }

        public void UpdateNextChargeDate(DateTime nextDate)
        {
            NextChargeDate = nextDate;
        }
    }
}