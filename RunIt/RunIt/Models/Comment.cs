using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunIt.Models
{
    class Comment
    {
        [JsonProperty("id")]
        public int id { set; get; }
        [JsonProperty("user_id")]
        public int user_id { set; get; }
        [JsonProperty("post_id")]
        public int post_id { set; get; }
        [JsonProperty("content")]
        public string content { set; get; }
        [JsonProperty("picture")]
        public string picture { set; get; }
        [JsonProperty("created_at")]
        public DateTime created_at { set; get; }
        [JsonProperty("updated_at")]
        public DateTime updated_at { set; get; }
    }

    class ListComment {
        [JsonProperty("comments")]
        public List<Comment> comments { set; get; }
    }


}
