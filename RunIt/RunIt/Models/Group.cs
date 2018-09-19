using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunIt.Models
{
    public class Group
    {
        [JsonProperty("id")]
        public int id;
        [JsonProperty("created_at")]
        public DateTime? created_at;
        [JsonProperty("updated_at")]
        public DateTime? updated_at;
        [JsonProperty("private_status")]
        public bool private_status;
        [JsonProperty("name")]
        public string name;
        [JsonProperty("user_id")]
        public int user_id;
        [JsonProperty("description")]
        public string description;
        [JsonProperty("is_register")]
        public bool is_register;
    }

    public class ListGroup
    {
        [JsonProperty("groups")]
        public List<Group> groups;
        [JsonProperty("status")]
        public string status;
    }
}
