using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MSMQNoSecurityService
{

    [ServiceContract]
    public interface IMSMQService
    {

        [OperationContract(IsOneWay = true)]
        void ShowMessage(string msg);
    }
}
