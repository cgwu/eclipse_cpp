using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;

namespace MyMsmqReceiver
{
    class Program
    {

        const string QueuePath = @".\Private$\MyQueue";

        static void Main(string[] args)
        {
            Console.WriteLine("Latest message");
            MessageQueue myQueue;
            myQueue = new MessageQueue(QueuePath);
            Message msg = myQueue.Receive();
            msg.Formatter = new BinaryMessageFormatter();
            Console.WriteLine(msg.Body.ToString());
            Console.ReadKey();
        }
    }
}
