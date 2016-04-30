using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClientConsumer.ServiceReference1;
using WebClientConsumer.localhost;
namespace WebClientConsumer
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Service1 obj = new Service1();
            Response.Write(obj.GetData(4220, true));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Service1Client obj = new Service1Client();
            //obj.ClientCredentials = System.Net.CredentialCache.DefaultCredentials;
            Response.Write(obj.GetData(4220));
        }
    }
}
