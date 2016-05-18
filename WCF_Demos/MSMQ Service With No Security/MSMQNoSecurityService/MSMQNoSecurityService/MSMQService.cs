using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace MSMQNoSecurityService
{
    public class MSMQService : IMSMQService
    {
        public void ShowMessage(string msg)
        {
            Debug.WriteLine(msg + " Begin Received at: " + System.DateTime.Now.ToString());
            Thread.Sleep(5000);
            Debug.WriteLine(msg + " End processed at: " + System.DateTime.Now.ToString());
        }
    }
}
