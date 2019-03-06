using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class Transfer
    {
        public int AccountId { get; set; }
        public double Amount { get; set; }
        public int AccountIdDestiny { get; set; }
        public string Key { get; set; }

        public Transfer() { }
        public Transfer(int accountId, double amount, int accountIdDestiny)
        {
            AccountId = accountId;
            Amount = amount;
            AccountIdDestiny = accountIdDestiny;
        }
        public Transfer(int accountId, double amount, int accountIdDestiny, string key)
        {
            AccountId = accountId;
            Amount = amount;
            AccountIdDestiny = accountIdDestiny;
            Key = key;
        }
    }
}
