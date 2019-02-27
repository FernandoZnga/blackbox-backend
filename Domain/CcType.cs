using System;
using System.Collections.Generic;

namespace Blackbox.Server.Domain
{
    public class CcType
    {
        public CcType() => CreatedAt = DateTime.Now;

        public int Id { get; set; }
        public List<Account> Accounts { get; set; }
        public int AccountId { get; set; }
        public string TypeName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
