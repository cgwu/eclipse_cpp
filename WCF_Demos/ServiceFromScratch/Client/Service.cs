using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Client
{
    [ServiceContract]
    public interface IHelloIndigoService
    {
        [OperationContract]
        string HelloIndigo();
    }
}
