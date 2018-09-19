using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunIt.Models
{
    public class Run
    {
        [JsonProperty("id")]
        public int? id { get; set; }
        [JsonProperty("coordinates")]
        public string coordinate { get; set; }
        //public List<Coordinate> coordinate { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
        [JsonProperty("started_at")]
        public DateTime started_at { get; set; }
        [JsonProperty("total_distance")]
        public double? total_distance { get; set; }
        [JsonProperty("total_time")]
        public double? total_time { get; set; }
        [JsonProperty("max_speed")]
        public double? max_speed { get; set; }
        [JsonProperty("user_id")]
        public int user_id { get; set; }
    }

    public class Coordinate
    {
        [JsonProperty("longitude")]
        public double longitude { get; set; }
        [JsonProperty("latitude")]
        public double latitude { get; set; }
        [JsonProperty("timestamp")]
        public DateTime? timestamp { get; set; }
    }

    public class ListRuns
    {
        [JsonProperty("runs")]
        public List<Run> runs { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
    }
}
