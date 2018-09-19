using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunIt.Models
{
    class Friendship
    {
        [JsonProperty("friends")]
        public List<Friends> friends { set; get; }
        [JsonProperty("requests")]
        public List<Request> requests { set; get; }
        [JsonProperty("notifications")]
        public List<Request> notifications { set; get; }

        [JsonProperty("status")]
        public string status { set; get; }
    }

    class Friends
    {
        [JsonProperty("id")]
        public int id { set; get; }
        [JsonProperty("firstname")]
        public string firstname { set; get; }
        [JsonProperty("lastname")]
        public string lastname { set; get; }
        [JsonProperty("friend_request_id")]
        public int friend_request_id { set; get; }
    }


    class Request
    {
        [JsonProperty("id")]
        public int id { set; get; }
        [JsonProperty("firstname")]
        public string firstname { set; get; }
        [JsonProperty("lastname")]
        public string lastname { set; get; }
        [JsonProperty("requests_id")]
        public int requests_id { set; get; }
    }

    class Notifications
    {
        [JsonProperty("notifications")]
        public string notifications { set; get; }
        [JsonProperty("status")]
        public string status { set; get; }
    }

}
