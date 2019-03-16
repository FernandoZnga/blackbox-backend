using Blackbox.Server.DataConn;
using Blackbox.Server.Domain;

namespace Blackbox.Server.Prop
{
    public class Log
    {
        //private static DataContext _context = new DataContext();

        public static void Save(__TextLog textLog)
        {
            using (var context = new DataContext())
            {
                context.__TextLogs.Add(textLog);
                context.SaveChanges();
            }
        }
    }
}
