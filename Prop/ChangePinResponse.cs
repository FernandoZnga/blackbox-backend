using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class ChangePinResponse
    {
        public int AccountId { get; set; }
        public int Response { get; set; }
        public string Key { get; set; }

        public ChangePinResponse() { }
        public ChangePinResponse(int accountId, int response)
        {
            AccountId = accountId;
            Response = response;
        }
        public ChangePinResponse(int accountId, int response, string key)
        {
            AccountId = accountId;
            Response = response;
            Key = key;
        }
    }
}
