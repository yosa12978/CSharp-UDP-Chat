using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UDP_Chat_С_
{
    class Program
    {
        static string remoteAddress;
        static int remotePort;
        static int localPort;
 
        static void Main(string[] args)
        {
            Console.Clear();

            Console.Write("Введите порт для прослушивания: ");
            localPort = Int32.Parse(Console.ReadLine());
            Console.Write("Введите удаленный адрес для подключения: ");
            remoteAddress = Console.ReadLine();
            Console.Write("Введите порт для подключения: ");
            remotePort = Int32.Parse(Console.ReadLine());
                 
            Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
            receiveThread.Start();
            SendMessage();
        }

        private static void SendMessage()
        {
            UdpClient client = new UdpClient();
            while(true)
            {
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                client.Send(data, data.Length, remoteAddress, remotePort);
            }
        }
 
        private static void ReceiveMessage()
        {
            UdpClient client = new UdpClient(localPort);
            IPEndPoint remoteIp = null;
            while(true)
            {
                byte[] data = client.Receive(ref remoteIp);
                string message = Encoding.Unicode.GetString(data);
                Console.WriteLine("Собеседник: {0}", message);
            }
        }
    }
}