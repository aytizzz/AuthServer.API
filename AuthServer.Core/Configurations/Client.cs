using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Configurations
{
    //AuthService istek yapicak uygulamalar(clientler) karsilik gelicek
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public List<string> Audiences { get; set; } // gonderecegim token icinde hansi apilere istek yapa bilir onu tutucaz

    }
}
