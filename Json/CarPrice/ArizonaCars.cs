using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ssLoader.Json.CarPrice
{
    public class ArizonaCars
    {
        [JsonProperty("Infernus(Владелец)")]
        public string Infernus { get; set; }
        [JsonProperty("Bullet(Владелец)")]
        public string Bullet { get; set; }
    }
}
