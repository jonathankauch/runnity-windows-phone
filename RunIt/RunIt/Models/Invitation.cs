using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RunIt.Models
{
    public class Invitation
    {
        [JsonProperty("id")]
        public int id { set; get; }
        [JsonProperty("invitation_type")]
        public int invitation_type { set; get; }
        [JsonProperty("status")]
        public string status { set; get; }
        [JsonProperty("created_at")]
        public DateTime created_at { set; get; }
        [JsonProperty("updated_at")]
        public DateTime updated_at { set; get; }
        [JsonProperty("user_id")]
        public int user_id { set; get; }
        [JsonProperty("receiver_id")]
        public string receiver_id { set; get; }
    }

    public class ListInvitation
    {
        [JsonProperty("invitations")]
        public List<Invitation> invitations { get; set; }
        [JsonProperty("status")]
        public int status { set; get; }
    }
}
