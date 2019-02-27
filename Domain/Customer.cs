using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackbox.Server.Domain
{
    public class Customer
    {
        public Customer() => CreatedAt = DateTime.Now;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public List<Account> Account { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
