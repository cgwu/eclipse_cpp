using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(HelloIndigo.HelloIndigoService),
                new Uri("http://localhost:7898/HelloIndigo")))
            {
                //host.AddServiceEndpoint(typeof(HelloIndigo.IHelloIndigoService),
                //    new BasicHttpBinding(), "HelloIndigoService");
                host.AddServiceEndpoint(typeof(HelloIndigo.IHelloIndigoService),
                    new WSHttpBinding(), "HelloIndigoServiceWS");

                host.Open();

                Console.WriteLine("Press <ENTER> to terminate the service host");
                Console.ReadLine();
            }
        }
    }
}
