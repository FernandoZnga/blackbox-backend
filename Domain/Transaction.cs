using System;

namespace Blackbox.Server.Domain
{
    public class Transaction
    {
        public Transaction()
        {
            CreatedAt = DateTime.Now;
            BillingId = 0;
        }

        public int Id { get; set; }
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public double BalanceBefore { get; set; }
        public double BalanceAfter { get; set; }
        public double Amount { get; set; }
        public TxType TxType { get; set; }
        public int TxTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AccountTypeName { get; set; }
        public string AtmId { get; set; }
        public string BillingName { get; set; }
        public int BillingId { get; set; }
    }
}
