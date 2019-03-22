using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace ClientCalculator
{
    class Program
    {
       

        static void Main(string[] args)
        {
            Console.WriteLine("Write Server IDAddress");

            string IpAddress = Console.ReadLine();

            Console.WriteLine("Write Server Process Port");

            string Port = Console.ReadLine();

            Client client = new Client(IpAddress, Port);

            while(true)
            {
                client.Send();
                client.Recieve();
            }
        }   
    }
}
