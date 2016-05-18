using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Diagnostics;

namespace MSMQSecuredService
{
   
    public class SecuredMSMQService : ISecuredMSMQService
    {
        public void ShowSecureMessage(string msg)
        {
            Debug.WriteLine(msg + " Signed & Secured at: " + System.DateTime.Now.ToString());
        }

        
    }
}
