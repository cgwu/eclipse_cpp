using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class MainClassEx
    {
        static void Main(string[] args)
        {
            HelloIndigoExService.HelloIndigoExServiceClient client =
                /* 使用basicHttpBinding */
                //new HelloIndigoExService.HelloIndigoExServiceClient("BasicHttpBinding_IHelloIndigoExService");
                /* 使用netTcpBinding */
                new HelloIndigoExService.HelloIndigoExServiceClient("NetTcpBinding_IHelloIndigoExService");
            string s = client.GetMessage("吴xx");
            Console.WriteLine(s);
            Console.WriteLine("Press <ENTER> to terminate Client.");
            Console.ReadLine();
        }
    }
}
