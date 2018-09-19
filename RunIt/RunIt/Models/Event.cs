using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunIt.Models
{
    public class Event
    {
        /** Attributes **/
        [JsonProperty("id")]
        public int id { set; get; }
        [JsonProperty("name")]
        public string name { set; get; }
        [JsonProperty("description")]
        public string description { set; get; }
        [JsonProperty("start_date")]
        public DateTime? start_date { set; get; }
        [JsonProperty("end_date")]
        public DateTime? end_date { set; get; }
        [JsonProperty("city")]
        public string city { set; get; }

       [JsonProperty("private_status")]
        public bool _private { set; get; }
        [JsonProperty("distance")]
        public double? distance { set; get; }

        [JsonProperty("created_at")]
        public DateTime created_at { set; get; }
        [JsonProperty("updated_at")]
        public DateTime updated_at { set; get; }
        [JsonProperty("user_id")]
        public int? user_id { set; get; }
    }

    public class ListEvent
    {
        [JsonProperty("events")]
        public List<Event> events { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
    }
}
