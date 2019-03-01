using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class AccountBalanceResponse
    {
        public int AccountId { get; set; }
        public double Balance { get; set; }
        public int Response { get; set; }

        public AccountBalanceResponse() { }
        public AccountBalanceResponse(int accountId, double balance, int response)
        {
            AccountId = accountId;
            Balance = balance;
            Response = response;
        }
    }
}
