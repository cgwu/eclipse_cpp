using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //EndpointAddress ep = new EndpointAddress("http://localhost:7897/HelloIndigo/HelloIndigoService");
            //IHelloIndigoService proxy = ChannelFactory<IHelloIndigoService>.CreateChannel(new BasicHttpBinding(), ep);

            EndpointAddress ep = new EndpointAddress("http://localhost:7897/HelloIndigo/HelloIndigoServiceWS");
            IHelloIndigoService proxy = ChannelFactory<IHelloIndigoService>.CreateChannel(new WSHttpBinding(), ep);
            string s = proxy.HelloIndigo();
            Console.WriteLine(s);
            Console.WriteLine("Press <ENTER> to terminate Client.");
            Console.ReadLine(); 
        }
    }
}
