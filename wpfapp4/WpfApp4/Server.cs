using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows;

namespace WpfApp4
{
    class Server
    {
        private static readonly Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int PORT = 100;

        public static void ConnectToServer()
        {
            while (!ClientSocket.Connected)
            {
                try
                {
                    ClientSocket.Connect("188.137.79.230", PORT);
                }
                catch (SocketException)
                {

                }
            }
        }

        public static void SendString(string text)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }         
        }

        public static string ReceiveResponse()
        {
            try
            {
                var buffer = new byte[2048*32];
                int received = ClientSocket.Receive(buffer, SocketFlags.None);
                if (received == 0)
                {
                    return null;
                }
                var data = new byte[received];
                Array.Copy(buffer, data, received);
                string text = Encoding.UTF8.GetString(data);
                return text;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return null;
            }
        }
        public static void Exit()
        {
            SendString("exit");
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
        }

    }
}
