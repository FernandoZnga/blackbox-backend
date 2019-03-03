using System;
using System.Collections.Generic;

namespace Blackbox.Server.Domain
{
    public class Account
    {
        public Account()
        {
            Transactions = new List<Transaction>();
            Balance = 0;
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public CreditCard CreditCard { get; set; }
        public CcType CcType { get; set; }
        public int CcTypeId { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
