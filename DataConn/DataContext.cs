using Microsoft.EntityFrameworkCore;

namespace Blackbox.Server.DataConn
{
    public class DataContext : DbContext
    {
        public DbSet<Domain.__TextLog> __TextLogs { get; set; }
        public DbSet<Domain.Customer> Customers { get; set; }
        public DbSet<Domain.CreditCard> CreditCards { get; set; }
        public DbSet<Domain.CcType> CcTypes { get; set; }
        public DbSet<Domain.Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Blackbox;Trusted_Connection=True;");
}
    }
}
