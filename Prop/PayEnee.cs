using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class PayEnee
    {
        public int BillId { get; set; }
        public int AccountId { get; set; }
        public string AtmId { get; set; }
        public string Key { get; set; }

        public PayEnee() { }
        public PayEnee(int billId, int accountId, string atmId)
        {
            BillId = billId;
            AccountId = accountId;
            AtmId = atmId;
        }
        public PayEnee(int billId, int accountId, string atmId, string key)
        {
            BillId = billId;
            AccountId = accountId;
            AtmId = atmId;
            Key = key;
        }
    }
}
