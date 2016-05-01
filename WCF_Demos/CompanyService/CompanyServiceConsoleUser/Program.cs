using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyServiceConsoleUser
{
    class Program
    {
        static void Main(string[] args)
        {
            CompanyService.CompanyServiceClient client = new CompanyService.CompanyServiceClient("BasicHttpBinding_ICompanyService");
            var info = client.GetInformation();
            Console.WriteLine(info);
            client.Close();
            Console.WriteLine("----------------------");

            CompanyService.CompanyConfidentialServiceClient client2 =
                new CompanyService.CompanyConfidentialServiceClient("NetTcpBinding_ICompanyConfidentialService");
            var info2 = client2.GetConfidentialInformation();
            Console.WriteLine(info2);

            Console.ReadKey();
        }
    }
}
