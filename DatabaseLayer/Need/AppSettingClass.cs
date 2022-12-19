using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Need
{
    public class AppSettingClass
    {
        public ConnectionStrings connectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnectionString { get; set; }

        public string DefaultConnectionString0 { get; set; }
    }
}
