using Newtonsoft.Json;
using RunIt.Models;

namespace RunIt.Models
{
    class Login
    {
        [JsonProperty("code")]
        public string Code { set; get; }
        [JsonProperty("auth_token")]
        public string auth_token { set; get; }
        [JsonProperty("success")]
        public bool success { set; get; }
        [JsonProperty("message")]
        public string message { set; get; }
        [JsonProperty("status")]
        public string status { set; get; }
        [JsonProperty("User")]
        public User User { set; get; }
    }  
} 