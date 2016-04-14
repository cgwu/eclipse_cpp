
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRedisCli
{
    class Program
    {
        static void Main(string[] args)
        {
            Test_ServiceStackRedis.TestRedis();
            Console.WriteLine("------------");
            Test_StackExchangeRedisStrongName.TestRedis();

            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }
}
