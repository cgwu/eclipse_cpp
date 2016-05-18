using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;

namespace MyMsmqSender
{
    class Program
    {
        const string QueuePath = @".\Private$\MyQueue";

        static void Main(string[] args)
        {
            Console.WriteLine("Enter Message to be sent");
            Console.WriteLine("(High priority messages should start with 'HP:'");
            string msgToBeSent = Console.ReadLine();

            // Get or Create Queue
            MessageQueue myQueue;
            if (MessageQueue.Exists(QueuePath))
            {
                myQueue = new MessageQueue(QueuePath);
            }
            else
            {
                myQueue = MessageQueue.Create(QueuePath);
            }

            // Create Message
            Message msg = new Message();
            // Set Formatter for Message
            msg.Formatter = new BinaryMessageFormatter();
            msg.Body = msgToBeSent;
            msg.Label = "App1Message";

            if (msgToBeSent.StartsWith("HP:"))
            {
                msg.Priority = MessagePriority.Highest;
            }
            else
            {
                msg.Priority = MessagePriority.Normal;
            }
            myQueue.Send(msg);
            Console.WriteLine("Thanks for sending message");
            Console.ReadKey();
        }
    }
}
