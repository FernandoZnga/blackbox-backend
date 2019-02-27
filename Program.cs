using Blackbox.Server.DataConn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackbox.Server
{
    internal static class Program
    {
        private static DataContext _context = new DataContext();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Main());
            SocketConn.AsynchronousSocketListener.StartListening();
        }

        private static void MatchCreditCardAtmLogin (CcPinNumber ccPinNumber)
        {
            //var ccNumber =  _context.
        }
    }
}
