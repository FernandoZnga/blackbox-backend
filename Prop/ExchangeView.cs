using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class ExchangeView
    {
        public string Currency { get; set; }
        public string AtmId { get; set; }
        public string Key { get; set; }

        public ExchangeView()
        {

        }
        public ExchangeView(string currency, string atmId)
        {
            Currency = currency;
            AtmId = atmId;
        }
        public ExchangeView(string currency, string atmId, string key)
        {
            Currency = currency;
            AtmId = atmId;
            Key = key;
        }
    }
}
