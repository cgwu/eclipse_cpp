using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HelloIndigo
{

    [ServiceContract]
    public interface IHelloIndigoService
    {
        [OperationContract]
        string HelloIndigo();
    }


    public class HelloIndigoService: IHelloIndigoService
    {
        public string HelloIndigo()
        {
            Console.WriteLine("new client called");
            return "Hello Indigo:" + DateTime.Now.ToString();
        }
    }

}
