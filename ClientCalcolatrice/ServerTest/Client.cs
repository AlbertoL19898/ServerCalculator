using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace ClientCalculator
{
    public enum typeOfOperator
    {
        division = 0,
        multiplication = 1,
        addition = 2,
        subtraction = 3,
        modulo = 4
    }


    class Client
    {
        private Socket socket;
        private IPEndPoint endPoint;

        private bool operatorIsCorrect;

        public Client(string address, string port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            endPoint = new IPEndPoint(IPAddress.Parse(address), int.Parse(port));
        }

        public void Send()
        {
            byte[] data = Process();
            socket.SendTo(data, endPoint);
        }

        public void Recieve() //Receives result package
        {
            byte[] data = new byte[256];

            EndPoint endPointM = new IPEndPoint(0, 0);

            socket.ReceiveFrom(data, ref endPointM);

            float result = BitConverter.ToSingle(data, 0);

            Console.WriteLine("The Result is: " + result);
            Console.WriteLine("_____________________________________________");
        }

        public byte[] Process()
        {
            byte byteOperator = 0;
            operatorIsCorrect = false;

            while (operatorIsCorrect == false)
            {
                Console.WriteLine("Write an operator( / , + , * , - , %)");
                string stringOperator = Console.ReadLine();

                switch (stringOperator)
                {
                    case "+":
                        byteOperator = (byte)typeOfOperator.addition;
                        operatorIsCorrect = true;
                        break;
                    case "*":
                        byteOperator = (byte)typeOfOperator.multiplication;
                        operatorIsCorrect = true;
                        break;
                    case "-":
                        byteOperator = (byte)typeOfOperator.subtraction;
                        operatorIsCorrect = true;
                        break;
                    case "/":
                        byteOperator = (byte)typeOfOperator.division;
                        operatorIsCorrect = true;
                        break;
                    case "%":
                        byteOperator = (byte)typeOfOperator.division;
                        operatorIsCorrect = true;
                        break;
                    default: continue;

                }
            }

            Console.WriteLine("Write first number");
            string firstNumber = Console.ReadLine();

            Console.WriteLine("Write second number");
            string secondNumber = Console.ReadLine();

            Packet packet = new Packet(byteOperator, float.Parse(firstNumber), float.Parse(secondNumber));

            return packet.GetData();
        }
    }
}
