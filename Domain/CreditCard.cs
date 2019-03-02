using System;

namespace Blackbox.Server.Domain
{
    public class CreditCard
    {
        public CreditCard() => CreatedAt = DateTime.Now;

        public int Id { get; set; }
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public string CcNumber { get; set; }
        public string PinNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
