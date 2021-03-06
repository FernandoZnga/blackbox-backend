﻿using System;

namespace Blackbox.Server.Domain
{
    public class Enee
    {
        public Enee()
        {
            Status = 0;
            CreatedAt = DateTime.Now;
            AccountId = 0;
        }

        public int Id { get; set; }
        public double BillAmount { get; set; }
        public int Status { get; set; }
        public int AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
