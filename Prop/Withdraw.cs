using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class Withdraw
    {
        public int AccountId { get; set; }
        public double Amount { get; set; }
        public string Key { get; set; }

        public Withdraw() { }
        public Withdraw(int accountId) => AccountId = AccountId;
        public Withdraw(int accountId, string key)
        {
            AccountId = accountId;
            Key = key;
        }
    }
}
