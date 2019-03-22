using System;
using System.Collections.Generic;

namespace Blackbox.Server.Domain
{
    public class AccountType
    {
        public AccountType()
        {
            Accounts = new List<Account>();
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public List<Account> Accounts { get; set; }
        public string TypeName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
