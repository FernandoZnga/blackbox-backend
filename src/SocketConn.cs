using Blackbox.Server.Domain;
using Blackbox.Server.Prop;
using Blackbox.Server.src;
using System;
using System.Net;  
using System.Net.Sockets;  
using System.Text;  
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackbox.Server
{
    public class SocketConn
    {
        // State object for reading client data asynchronously  
        public static Socket wSocket;

        public class StateObject {
            // Client  socket.  
		    public Socket workSocket = null;
		    // Size of receive buffer.  
		    public const int BufferSize = 1024;  
		    // Receive buffer.  
		    public byte[] buffer = new byte[BufferSize];  
		    // Received data string.  
		    public StringBuilder sb = new StringBuilder();    
		}  
		  
		public class AsynchronousSocketListener {  
            public void Start()
            {
                StartListening();
                ShowMessageBox(true);
            }

            // Thread signal.  
            public static ManualResetEvent allDone = new ManualResetEvent(false);

            public AsynchronousSocketListener() {  
		    }  
		  
		    public async void StartListening()
            {
                await Task.Run(() => Loop());
		    }  
            private async void Loop()
            {
                // Establish the local endpoint for the socket.  
                // The DNS name of the computer  
                // running the listener is "host.contoso.com".  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP socket.  
                Socket listener = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the local endpoint and listen for incoming connections.  
                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(100);

                    while (true)
                    {
                        // Set the event to nonsignaled state.  
                        allDone.Reset();

                        // Start an asynchronous socket to listen for connections.  
                        Console.WriteLine("Waiting for a connection...");
                        listener.BeginAccept(
                            new AsyncCallback(AcceptCallback),
                            listener);
                        // Wait until a connection is made before continuing.  
                        allDone.WaitOne();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                Console.WriteLine("\nPress ENTER to continue...");
                Console.Read();
            }
            private void ShowMessageBox(bool value)
            {
                if (value)
                {
                    MessageBox.Show("Server Started");
                }
                else
                {
                    MessageBox.Show("Server Stoped");
                }
            }
		  
		    public static void AcceptCallback(IAsyncResult ar) {  
		        // Signal the main thread to continue.  
		        allDone.Set();  
		        
		        // Get the socket that handles the client request.  
		        Socket listener = (Socket) ar.AsyncState;  
		        Socket handler = listener.EndAccept(ar);

                // Create the state object.  
                StateObject state = new StateObject
                {
                    workSocket = handler
                };
                handler.BeginReceive( state.buffer, 0, StateObject.BufferSize, 0,  
		            new AsyncCallback(ReadCallback), state);  
		    }  
		  
		    public static void ReadCallback(IAsyncResult ar) {  
		       string content = string.Empty;  
		  
		        // Retrieve the state object and the handler socket  
		        // from the asynchronous state object.  
		        StateObject state = (StateObject) ar.AsyncState;  
		        Socket handler = state.workSocket;
                Console.WriteLine(handler.AddressFamily.ToString());
                wSocket = handler;
                Console.WriteLine(wSocket.Connected);

                // Read data from the client socket.   
                int bytesRead = handler.EndReceive(ar);  
		  
		        if (bytesRead > 0) {  
		            // There  might be more data, so store the data received so far.
		            state.sb.Append(Encoding.ASCII.GetString(  
		                state.buffer, 0, bytesRead));  
		  
		            // Check for end-of-file tag. If it is not there, read   
		            // more data.  
		            content = state.sb.ToString();  
		            if (content.IndexOf("<EOF>") > -1) {
                        // All the data has been read from the   
                        // client. Display it on the console.  
                        content = content.Substring(0, content.IndexOf("<EOF>", 0));
                        Console.WriteLine("Read Encrypted {0} bytes from socket. \n Data : {1}",
                            content.Length, content);
                        // Here goes to action
                        //content = content.Substring(0, content.IndexOf("<EOF>", 0));
                        var contentText = Encryption.Decrypt(content, "Security1234");
                        Console.WriteLine("Read Decrypted {0} bytes from socket. \n Data : {1}",
                            contentText.Length, contentText);

                        var md5IN = contentText.Substring(contentText.IndexOf("<Key>", 0) + 5, contentText.IndexOf("</Key>", 0) - contentText.IndexOf("<Key>", 0) - 5);
                        var atmId = contentText.Substring(contentText.IndexOf("<AtmId>", 0) + 7, contentText.IndexOf("</AtmId>", 0) - contentText.IndexOf("<AtmId>", 0) - 7);
                        __TextLog logTextIN = new __TextLog
                        {
                            DesText = content,
                            XmlText = contentText,
                            Md5IN = md5IN,
                            AtmId = atmId
                        };
                        var responseContent = Handle.ReadText(contentText, logTextIN);
                        //
                        // Echo the data back to the client. 
                        var md5OUT = responseContent.Substring(responseContent.IndexOf("<Key>", 0) + 5, responseContent.IndexOf("</Key>", 0) - responseContent.IndexOf("<Key>", 0) - 5);
                        var transaction = responseContent.Substring(responseContent.IndexOf("<", 1) + 1, responseContent.IndexOf("xmlns:xsi", 0) - responseContent.IndexOf("<", 1) - 1);
                        var responseEncrypted = Encryption.Encrypt(responseContent, "Security1234");
                        __TextLog logTextOUT = new __TextLog
                        {
                            DesText = responseEncrypted,
                            XmlText = responseContent,
                            Direction = "OUT",
                            Md5OUT = md5OUT,
                            Transaction = transaction,
                            AtmId = atmId
                        };
                        Log.SaveOut(logTextOUT);
                        Console.WriteLine("Sent Xml text {0} bytes from socket. \n Data : {1}",
                            content.Length, responseContent);
                        Console.WriteLine("Sent Encrypted {0} bytes from socket. \n Data : {1}",
                            content.Length, responseEncrypted);
                        Send(handler, responseEncrypted);  
		            } else {  
		                // Not all data received. Get more.  
		                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,  
		                new AsyncCallback(ReadCallback), state);  
		            }  
		        }  
		    }  
		  
		    private static void Send(Socket handler, string data) {  
		        // Convert the string data to byte data using ASCII encoding.  
		        byte[] byteData = Encoding.ASCII.GetBytes(data);  
		  
		        // Begin sending the data to the remote device.  
		        handler.BeginSend(byteData, 0, byteData.Length, 0,  
		            new AsyncCallback(SendCallback), handler);  
		    }  
		  
		    private static void SendCallback(IAsyncResult ar) {  
		        try {  
		            // Retrieve the socket from the state object.  
		            Socket handler = (Socket) ar.AsyncState;
                    Console.WriteLine("Remote: {0}", handler.RemoteEndPoint.ToString());
                    Console.WriteLine("Local {0}", handler.LocalEndPoint.ToString());

                    // Complete sending the data to the remote device.  
                    int bytesSent = handler.EndSend(ar);  
		            Console.WriteLine("Sent {0} bytes to client.", bytesSent);  
		    
		            handler.Shutdown(SocketShutdown.Both);  
		            handler.Close();  
		  
		        } catch (Exception e) {  
		            Console.WriteLine(e.ToString());  
		        }  
		    }

            //public static int Main(String[] args) {  
            //    StartListening();  
            //    return 0;  
            //}  
        }  
    }
}
