using Microsoft.EntityFrameworkCore;

namespace Blackbox.Server.DataConn
{
    public class DataContext : DbContext
    {
        private DbSet<Domain.__TextLog> __TextLogs { get; set; }
        private DbSet<Domain.Customer> Customers { get; set; }
        private DbSet<Domain.CreditCard> CreditCards { get; set; }
        private DbSet<Domain.CcType> CcTypes { get; set; }
        private DbSet<Domain.Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Blackbox;Trusted_Connection=True;");
}
    }
}
