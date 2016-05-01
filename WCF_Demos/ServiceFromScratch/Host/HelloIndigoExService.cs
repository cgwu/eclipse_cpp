using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Host
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“HelloIndigoExService”。
    public class HelloIndigoExService : IHelloIndigoExService
    {
        public string GetMessage(string name)
        {
            string msg = DateTime.Now.ToString() + ",欢迎 " + name;
            Console.WriteLine(msg);
            return msg;
        }
    }
}
