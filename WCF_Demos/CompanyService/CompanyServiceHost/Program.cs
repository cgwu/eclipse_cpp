using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace CompanyServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using(ServiceHost host = new ServiceHost(typeof(CompanyService.CompanyService)))
            {
                host.Open();

                Console.WriteLine("Service start at: " + DateTime.Now.ToString());
                Console.ReadLine();
            }
        }
    }
}
