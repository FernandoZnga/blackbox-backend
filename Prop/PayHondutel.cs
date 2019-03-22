using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class PayHondutel
    {
        public int BillId { get; set; }
        public int AccountId { get; set; }
        public string AtmId { get; set; }
        public string Key { get; set; }

        public PayHondutel() { }
        public PayHondutel(int billId, int accountId, string atmId)
        {
            BillId = billId;
            AccountId = accountId;
            AtmId = atmId;
        }
        public PayHondutel(int billId, int accountId, string atmId, string key)
        {
            BillId = billId;
            AccountId = accountId;
            AtmId = atmId;
            Key = key;
        }
    }
}
