using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 100;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];
        private static DBConnection db = new DBConnection();

        static void Main()
        {
            Console.Title = "Server";
            SetupServer();

            string exit;
            while (true)
            {
                exit = Console.ReadLine();
                if (exit == "exit")
                {
                    CloseAllSockets();
                    Environment.Exit(0);
                }
            }
        }

        private static void SetupServer()
        {
            Console.WriteLine("Setting up server...");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Server setup complete");
        }

        /// <summary>
        /// Close all connected client (we do not need to shutdown the server socket as its connections
        /// are already closed with the clients).
        /// </summary>
        private static void CloseAllSockets()
        {
            foreach (Socket socket in clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            try
            {
                Socket socket;

                try
                {
                    socket = serverSocket.EndAccept(AR);
                }
                catch (ObjectDisposedException)
                {
                    return;
                }

                clientSockets.Add(socket);
                socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
                Console.WriteLine("Client connected, waiting for request...");
                serverSocket.BeginAccept(AcceptCallback, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            try
            {


                Socket current = (Socket)AR.AsyncState;
                int received;

                try
                {
                    received = current.EndReceive(AR);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Client forcefully disconnected");
                    current.Close();
                    clientSockets.Remove(current);
                    return;
                }

                byte[] recBuf = new byte[received];
                Array.Copy(buffer, recBuf, received);
                string text = Encoding.UTF8.GetString(recBuf);
                Console.WriteLine("Received request: " + text);

                string[] request = text.Split(' ');
                string answer;
                byte[] data;

                switch (request[0])
                {
                    default:
                        Console.WriteLine("Invalid request.");
                        data = Encoding.UTF8.GetBytes("Invalid request.");
                        current.Send(data);
                        Console.WriteLine("Warning Sent.");
                        break;

                    case "register":
                        answer = db.Register(request[1], request[2], request[3], request[4], request[5], request[6], request[7], request[8], request[9], request[10], request[11]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;

                    case "login":
                        answer = db.Login(request[1], request[2]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;

                    case "show_category":
                        answer = db.Show_category(request[1]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "show_product":
                        answer = db.Show_product(request[1]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "show_user":
                        answer = db.Show_user(request[1]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "cart_add":
                        answer = db.Cart_add(request[1], request[2], request[3]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "cart_show":
                        answer = db.Cart_show(request[1]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "cart_delete":
                        answer = db.Cart_delete(request[1]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "alter_user":
                        answer = db.Alter_user(request[1], request[2], request[3], request[4], request[5], request[6], request[7], request[8], request[9], request[10], request[11]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "product_add":
                        answer = db.Product_add(request[1], request[2], request[3], request[4], request[5], request[6]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "order_add":
                        answer = db.Order_add(request[1], request[2], request[3], request[4], request[5]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "order_show":
                        answer = db.Order_show();
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "alter_order":
                        answer = db.Alter_order(request[1], request[2], request[3]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "product_delete":
                        answer = db.Product_delete(request[1]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "product_search":
                        answer = db.Product_search(request[1]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "promotion":
                        answer = db.Promotion(request[1],request[2]);
                        data = Encoding.UTF8.GetBytes(answer);
                        current.Send(data);
                        Console.WriteLine("Answer sent: " + answer);
                        break;
                    case "exit":
                        current.Shutdown(SocketShutdown.Both);
                        current.Close();
                        clientSockets.Remove(current);
                        Console.WriteLine("Client disconnected");
                        return;


                }

                current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
