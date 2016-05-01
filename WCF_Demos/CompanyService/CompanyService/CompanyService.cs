using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CompanyService
{
    public class CompanyService : ICompanyService , ICompanyConfidentialService
    {
        public string GetConfidentialInformation()
        {
            return "内部人员才能看到的信息";
        }

        public string GetInformation()
        {
            return "公司公共信息";
        }
    }
}
