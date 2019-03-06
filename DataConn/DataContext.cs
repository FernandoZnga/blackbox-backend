using Blackbox.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blackbox.Server.DataConn
{
    public class DataContext : DbContext
    {
        public DbSet<__TextLog> __TextLogs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<CcType> CcTypes { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TxType> TxTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Blackbox;Trusted_Connection=True;");
}
    }
}
