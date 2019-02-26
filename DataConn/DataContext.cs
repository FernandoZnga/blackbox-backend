using Microsoft.EntityFrameworkCore;

namespace Blackbox.Server.DataConn
{
    public class DataContext : DbContext
    {
        private DbSet<Domain._TextLog> _textLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
}
    }
}
