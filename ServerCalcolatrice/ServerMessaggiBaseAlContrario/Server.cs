using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCalculator
{
    
    public enum typeOfOperator
    {
        division,
        multiplication,
        addition,
        subtraction,
        modulo
    }

    public class Server
    {
        private Itransport transport;
       

        public Server(Itransport gameTransport)
        {
            transport = gameTransport;
        }

        public void Start()
        {
            Console.WriteLine("Server Started");

            while (true)
            {
                SingleStep();
            }
        }

        public void SingleStep()
        {
            EndPoint sender = transport.CreateEndPoint();
            byte[] data = transport.Recv(256, ref sender);

            if (data != null)
            {
                Packet packToSend = Process(data);
                
                if(Send(packToSend, sender))
                {
                    Console.WriteLine("Result send to " + sender);
                }
                else
                {
                    Console.WriteLine("Impossible to send result");
                }
            }
        }

        public bool Send(Packet packet, EndPoint endPoint)
        {
            return transport.Send(packet.GetData(), endPoint);
        }

        public Packet Process(byte[] data)
        {
            int operatore = data[0];

            float firstNumber = BitConverter.ToSingle(data, 1);
            float secondNumber = BitConverter.ToSingle(data, 5);

            float result = CheckAndDoOperation(operatore, firstNumber, secondNumber);

            Packet packet = new Packet(result);

            return packet;
        }

        public float CheckAndDoOperation(int op,float fn,float sn)
        {
            float result;

            switch (op)
            {
                case (int)(typeOfOperator.division):
                    result = fn / sn;
                    break;
                case (int)(typeOfOperator.multiplication):
                    result = fn *sn;
                    break;
                case (int)(typeOfOperator.subtraction):
                    result = fn - sn;
                    break;
                case (int)(typeOfOperator.addition):
                    result = fn + sn;
                    break;
                case (int)(typeOfOperator.modulo):
                    result = fn % sn;
                    break;
                default:
                    result = 0.0001f;
                    break;
                    
            }

            return result;
        }
    }
}
