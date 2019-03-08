using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class TransferResponse
    {
        public int AccountId { get; set; }
        public double NewBalance { get; set; }
        public string AccountTypeName { get; set; }
        public int AccountIdDestiny { get; set; }
        public int Response { get; set; }
        public string Key { get; set; }

        public TransferResponse() { }
        public TransferResponse(int accountId, double newBalance, string accountTypeName, int accountIdDestiny, int response)
        {
            AccountId = accountId;
            NewBalance = newBalance;
            AccountTypeName = accountTypeName;
            AccountIdDestiny = accountIdDestiny;
            Response = response;
        }
        public TransferResponse(int accountId, double newBalance, string accountTypeName, int accountIdDestiny, int response, string key)
        {
            AccountId = accountId;
            NewBalance = newBalance;
            AccountTypeName = accountTypeName;
            AccountIdDestiny = accountIdDestiny;
            Response = response;
            Key = key;
        }
    }
}
