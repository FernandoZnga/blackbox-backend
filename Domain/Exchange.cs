using System;

namespace Blackbox.Server.Domain
{
    public class Exchange
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public double Compra { get; set; }
        public double Venta { get; set; }
        public DateTime CreatedAt { get; set; }

        public Exchange()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
