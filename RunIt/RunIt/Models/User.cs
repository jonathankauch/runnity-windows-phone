using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunIt.Models
{
    public class User
    {
        public User() {}
        public User(string e, string p) { email = e; password = p; }

        public void Copy(User o)
        {
            this.firstname = o.firstname;
            this.lastname = o.lastname;
            this.email = o.email;
            this.token = o.token;
//            this.password = o.password;
            this.enable = o.enable;
            this.id = o.id;
            this.phone = o.phone;
            this.address = o.address;
        }

        /** Attributes **/
        [JsonProperty("id")]
        public int id { set; get; }
        [JsonProperty("authentication_token")]
        public string token { set; get; }
        [JsonProperty("email")]
        public string email { set; get; }
        [JsonProperty("created_at")]
        public string created_at { set; get; }
        [JsonProperty("updated_at")]
        public string updated_at { set; get; }
        [JsonProperty("firstname")]
        public string firstname { set; get; }
        [JsonProperty("lastname")]
        public string lastname { set; get; }
        [JsonProperty("phone")]
        public string phone { set; get; }
        [JsonProperty("password")]
        public string password { set; get; }
        [JsonProperty("address")]
        public string address { set; get; }
        [JsonProperty("enable")]
        public string enable { set; get; }

        [JsonProperty("ranking")]
        public string ranking { set; get; }

    }

}
