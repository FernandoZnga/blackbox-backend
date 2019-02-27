using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackbox.Server.Domain
{
    public class Account
    {
        public Account() => CreatedAt = DateTime.Now;

        public int Id { get; set; }
        public Customer Customer { get; set; }
        public CreditCard CreditCard { get; set; }
        public double Balance { get; set; }
        public int CcType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
