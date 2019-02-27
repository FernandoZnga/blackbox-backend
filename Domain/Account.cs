using System;

namespace Blackbox.Server.Domain
{
    public class Account
    {
        public Account() => CreatedAt = DateTime.Now;

        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public CreditCard CreditCard { get; set; }
        public double Balance { get; set; }
        public CcType CcType { get; set; }
        public int CcTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
