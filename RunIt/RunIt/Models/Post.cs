using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunIt.Models
{
    public class Post
    {
        [JsonProperty("id")]
        public int id { set; get; }
        [JsonProperty("owner")]
        public string owner { set; get; }
        [JsonProperty("picture")]
        public string picture { set; get; }
        [JsonProperty("created_at")]
        public DateTime created_at { set; get; }
        [JsonProperty("updated_at")]
        public DateTime updated_at { set; get; }
        [JsonProperty("content")]
        public string content { set; get; }
        [JsonProperty("user_id")]
        public int user_id { set; get; }
        [JsonProperty("event_id")]
        public string event_id { set; get; }
        [JsonProperty("group_id")]
        public string group_id { set; get; }
        [JsonProperty("like")]
        public string like { set; get; }
        [JsonProperty("user")]
        public PUser user;
    }

    public class PUser {
        [JsonProperty("user_id")]
        public int user_id { set; get; }
        [JsonProperty("full_name")]
        public string full_name { set; get; }
    }

    public class ListData
    {
        [JsonProperty("data")]
        public List<Post> data { set; get; }
    }

    public class ListPost
    {
        [JsonProperty("posts")]
        public List<Post> posts { set; get; }
        [JsonProperty("status")]
        public string status { set; get; }
    }
}


