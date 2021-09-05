using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primavera.Paymentsds.Mpesa.Models
{
    [JsonObject]
    public class UserResponse
    {
        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("access_token")]
        public string access_token { get; set; }
    }
}
