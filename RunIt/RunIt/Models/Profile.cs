using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunIt.Models
{
    class Profile
    {
        [JsonProperty("user")]
        public User User { set; get; }
    }
}
