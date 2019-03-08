using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class AccountBalance
    {
        public int AccountId { get; set; }
        public string AtmId { get; set; }
        public string Key { get; set; }

        public AccountBalance() { }
        public AccountBalance(int accountId, string atmId)
        {
            AccountId = AccountId;
            AtmId = AtmId;
        }

        public AccountBalance(int accountId, string atmId, string key)
        {
            AccountId = accountId;
            AtmId = AtmId;
            Key = key;
        }
    }
}
