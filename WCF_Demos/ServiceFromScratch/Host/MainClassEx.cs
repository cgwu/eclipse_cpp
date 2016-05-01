using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Host
{
    class MainClassEx
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Host.HelloIndigoExService)))
            {
                host.Open();

                Console.WriteLine("Press <ENTER> to terminate the service host");
                Console.ReadLine();
            }
        }
    }
}
