using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class GeneralResponse
    {
        public int Response { get; set; }
        public string Key { get; set; }

        public GeneralResponse() { }
        public GeneralResponse(int response) => Response = response;
        public GeneralResponse(int response, string key)
        {
            Response = response;
            Key = key;
        }
    }
}
