using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class MyTransactionsResponse
    {
        public int AccountId { get; set; }
        public int Response { get; set; }
        public string Key { get; set; }

        public MyTransactionsResponse() { }
        public MyTransactionsResponse(int accountId, int response)
        {
            AccountId = accountId;
            Response = response;
        }
        public MyTransactionsResponse(int accountId, int response, string key)
        {
            AccountId = accountId;
            Response = response;
            Key = key;
        }
    }
}
