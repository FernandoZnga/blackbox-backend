using Blackbox.Server.DataConn;
using Blackbox.Server.Domain;

namespace Blackbox.Server.Prop
{
    public class Log
    {
        //private static DataContext _context = new DataContext();

        public static void SaveIn(__TextLog textLog)
        {
            Main.UpdateIncomingFields(textLog);

            using (var context = new DataContext())
            {
                context.__TextLogs.Add(textLog);
                context.SaveChanges();
            }
        }
        public static void SaveOut(__TextLog textLog)
        {
            Main.UpdateOutgoingFields(textLog);

            using (var context = new DataContext())
            {
                context.__TextLogs.Add(textLog);
                context.SaveChanges();
            }
        }
    }
}
