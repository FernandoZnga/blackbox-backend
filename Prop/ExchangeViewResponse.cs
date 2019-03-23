using System;

namespace Blackbox.Server.Prop
{
    [Serializable()]
    public class ExchangeViewResponse
    {
        public string Currency { get; set; }
        public double Compra { get; set; }
        public double Venta { get; set; }
        public int Response { get; set; }
        public string Key { get; set; }

        public ExchangeViewResponse()
        {

        }
        public ExchangeViewResponse(string currency, double compra, double venta)
        {
            Currency = currency;
            Compra = compra;
            Venta = venta;
        }
        public ExchangeViewResponse(string currency, double compra, double venta, string key)
        {
            Currency = currency;
            Compra = compra;
            Venta = venta;
            Key = key;
        }
    }
}
