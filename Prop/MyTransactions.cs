using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class MyTransactions
    {
        public int AccountId { get; set; }
        public string AtmId { get; set; }
        public string Key { get; set; }

        public MyTransactions() { }
        public MyTransactions(int accountId, string atmId)
        {
            AccountId = accountId;
            AtmId = atmId;
        }

        public MyTransactions(int accountId, string atmId, string key)
        {
            AccountId = accountId;
            AtmId = atmId;
            Key = key;
        }
    }
}
