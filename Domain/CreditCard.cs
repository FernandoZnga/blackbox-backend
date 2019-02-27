using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackbox.Server.Domain
{
    public class CreditCard
    {
        public CreditCard() => CreatedAt = DateTime.Now;

        public int Id { get; set; }
        public string CcNumber { get; set; }
        public string PinNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
