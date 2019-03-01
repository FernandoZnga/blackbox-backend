using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class GeneralResponse
    {
        public int Response { get; set; }

        public GeneralResponse() { }
        public GeneralResponse(int response) => Response = response;
    }
}
