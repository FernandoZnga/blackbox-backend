using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class PaySanaaResponse
    {
        public int AccountId { get; set; }
        public double NewBalance { get; set; }
        public string AccountTypeName { get; set; }
        public int Response { get; set; }
        public string Key { get; set; }

        public PaySanaaResponse() { }
        public PaySanaaResponse(int accountId, double newBalance, string accountTypeName, int response)
        {
            AccountId = accountId;
            NewBalance = newBalance;
            AccountTypeName = accountTypeName;
            Response = response;
        }
        public PaySanaaResponse(int accountId, double newBalance, string accountTypeName, int response, string key)
        {
            AccountId = accountId;
            NewBalance = newBalance;
            AccountTypeName = accountTypeName;
            Response = response;
            Key = key;
        }
    }
}
