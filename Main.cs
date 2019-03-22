using System;
using System.Windows.Forms;
using Blackbox.Server.Domain;
using static Blackbox.Server.SocketConn;

namespace Blackbox.Server
{
    public partial class Main : Form
    {
        public static string DesTextStringIN { get; set; }
        public static string XmlTextStringIN { get; set; }
        public static string Md5TextStringIN { get; set; }
        public static string Md5TextStringCALC { get; set; }
        public static string Md5TextStringOUT { get; set; }
        public static string DesTextStringOUT { get; set; }
        public static string XmlTextStringOUT { get; set; }

        // Run Server
        AsynchronousSocketListener async = new AsynchronousSocketListener();
        bool startStop;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, System.EventArgs e)
        {
            // Run Server
            // AsynchronousSocketListener async = new AsynchronousSocketListener();
            // async.Start();
            startStop = false;
            
        }

        private void StartStop_Click(object sender, System.EventArgs e)
        {
            if (startStop)
            {
                this.Close();
                startStop = false;
            }
            else
            {
                async.Start();
                startStop = true;
            }
        }

        internal static void UpdateIncomingFields(__TextLog logTextIN)
        {
            DesTextStringIN = logTextIN.DesText;
            XmlTextStringIN = logTextIN.XmlText;
            Md5TextStringIN = logTextIN.Md5IN;
            Md5TextStringCALC = logTextIN.Md5OUT;
        }

        internal static void UpdateOutgoingFields(__TextLog textLog)
        {
            DesTextStringOUT = textLog.DesText;
            XmlTextStringOUT = textLog.XmlText;
            Md5TextStringOUT = textLog.Md5OUT;
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            DesText.Clear();
            DesText.Text = DesTextStringIN;
            XmlText.Clear();
            XmlText.Text = XmlTextStringIN;
            Md5In.Clear();
            Md5In.Text = Md5TextStringIN;
            Md5Calc.Clear();
            Md5Calc.Text = Md5TextStringCALC;

            DesTextOut.Clear();
            DesTextOut.Text = DesTextStringOUT;
            XmlTextOut.Clear();
            XmlTextOut.Text = XmlTextStringOUT;
            Md5Out.Clear();
            Md5Out.Text = Md5TextStringOUT;
        }
    }
}
