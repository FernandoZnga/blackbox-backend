using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class CcPinNumberResponse
    {
        public int Account { get; set; }
        public int Response { get; set; }

        public CcPinNumberResponse() { }
        public CcPinNumberResponse(int account, int response)
        {
            Account = account;
            Response = response;
        }
    }
}
