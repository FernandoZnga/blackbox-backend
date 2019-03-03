using System;
using System.Collections.Generic;

namespace Blackbox.Server.Domain
{
    public class TxType
    {
        public TxType()
        {
            Transactions = new List<Transaction>();
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public List<Transaction> Transactions { get; set; }
        public string TypeName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
