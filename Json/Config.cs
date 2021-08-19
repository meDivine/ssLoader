using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ssLoader.Json
{
   public class Config
    {
        public string api_key { get; set; }
        public string title { get; set; }
        public bool secret_money { get; set; }
        public bool secret_level { get; set; }
        public string seller_message { get; set; }
    }
}
