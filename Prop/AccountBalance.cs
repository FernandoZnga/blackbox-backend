using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class AccountBalance
    {
        public int AccountId { get; set; }
        public string Key { get; set; }

        public AccountBalance() { }
        public AccountBalance(int accountId) => AccountId = AccountId;
        public AccountBalance(int accountId, string key)
        {
            AccountId = accountId;
            Key = key;
        }
    }
}
