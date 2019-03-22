using System;
using NUnit.Framework;

namespace ServerCalculator.Test
{
    public class ServerCalculatorTest
    {
        private FakeTransport transport;
        private Server server;

        [SetUp]
        public void SetUpTests()
        {
            transport = new FakeTransport();
            server = new Server(transport);
        }

        [Test]
        public void TestSumPositive()
        {
            transport.ClientEnqueue(new Packet((byte)2, 5.0f, 5.0f), "User1", 9999);

            server.SingleStep();

            FakeData data = transport.ClientDequeue();
            float result = BitConverter.ToSingle(data.data, 0);
            Assert.That(result, Is.EqualTo(10.0f));

        }

        [Test]
        public void TestDivisionPositive()
        {
            transport.ClientEnqueue(new Packet((byte)0, 5.0f, 5.0f), "User1", 9999);

            server.SingleStep();

            FakeData data = transport.ClientDequeue();
            float result = BitConverter.ToSingle(data.data, 0);
            Assert.That(result, Is.EqualTo(1.0f));

        }

        [Test]
        public void TestMultiplicationPositive()
        {
            transport.ClientEnqueue(new Packet((byte)1, 5.0f, 5.0f), "User1", 9999);

            server.SingleStep();

            FakeData data = transport.ClientDequeue();
            float result = BitConverter.ToSingle(data.data, 0);
            Assert.That(result, Is.EqualTo(25.0f));

        }

        [Test]
        public void TestSubtractionPositive()
        {
            transport.ClientEnqueue(new Packet((byte)3, 7.0f, 5.0f), "User1", 9999);

            server.SingleStep();

            FakeData data = transport.ClientDequeue();
            float result = BitConverter.ToSingle(data.data, 0);
            Assert.That(result, Is.EqualTo(2));

        }

        [Test]
        public void TestModuloPositive()
        {
            transport.ClientEnqueue(new Packet((byte)4, 6.0f, 5.0f), "User1", 9999);

            server.SingleStep();

            FakeData data = transport.ClientDequeue();
            float result = BitConverter.ToSingle(data.data, 0);
            Assert.That(result, Is.EqualTo(1));

        }

        [Test]
        public void TestCapacityOfResultPacketPositive()
        {
            transport.ClientEnqueue(new Packet((byte)4, 6.0f, 5.0f), "User1", 9999);

            server.SingleStep();

            FakeData data = transport.ClientDequeue();
            float result = BitConverter.ToSingle(data.data, 0);
            Assert.That(data.data.Length, Is.EqualTo(4));

        }

        [Test]
        public void TestWrongOperatorPositive()
        {
            transport.ClientEnqueue(new Packet((byte)5, 6.0f, 5.0f), "User1", 9999);

            server.SingleStep();

            FakeData data = transport.ClientDequeue();
            float result = BitConverter.ToSingle(data.data, 0);
            Assert.That(result, Is.EqualTo(0.0001f));

        }

        [Test]
        public void TestNumberOfResultPacketsPositive()
        {
            transport.ClientEnqueue(new Packet((byte)5, 6.0f, 5.0f), "User1", 9999);
            server.SingleStep();
            transport.ClientEnqueue(new Packet((byte)2, 3.0f, 5.0f), "User1", 9999);
            server.SingleStep();
            transport.ClientEnqueue(new Packet((byte)1, 6.0f, 3.0f), "User1", 9999);
            server.SingleStep();

            FakeData data = transport.ClientDequeue();
            float result = BitConverter.ToSingle(data.data, 0);

            Assert.That(transport.NumOfPacket, Is.EqualTo(2));
   
        }
    }
}
