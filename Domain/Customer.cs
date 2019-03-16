using System;
using System.Collections.Generic;

namespace Blackbox.Server.Domain
{
    public class Customer
    {
        public Customer()
        {
            Accounts = new List<Account>();
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public List<Account> Accounts { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }
    }
}
