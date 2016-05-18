using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MSMQSecuredService
{
    
    [ServiceContract]
    public interface ISecuredMSMQService
    {

            [OperationContract(IsOneWay = true)]
            void ShowSecureMessage(string msg);
        
    }

   
   
}
