using Flurl.Http;
using Newtonsoft.Json;
using RunIt.Models;
using RunIt.Models.Singleton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace RunIt.Api
{
    class ApiSingleton
    {
        private const string URL = "https://runitv2.herokuapp.com/api/v1";
        private const string ERROR_CODE = "400";


        public static async void PopupMsg(string str)
        {
            MessageDialog msgbox = new MessageDialog(str);
            await msgbox.ShowAsync();
        }

        public static HttpBaseProtocolFilter GetFilter()
        {
            HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
            filter.AllowAutoRedirect = true;
            filter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
            filter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;
            return filter;
        }

        public static async Task<int> SignIn(string email, string password)
        {
            try
            {
                var filter = GetFilter();
                HttpClient client = new HttpClient();
                var content = new HttpFormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("password", password)
                });
                var r = await client.PostAsync(new Uri(URL + "/login"), content);
                var response = await r.Content.ReadAsInputStreamAsync();
                string jsonMessage;
                jsonMessage = new StreamReader(response.AsStreamForRead()).ReadToEnd();
                Login us = JsonConvert.DeserializeObject<Login>(jsonMessage);
                if (us.success == true)
                {
                    Singleton.Instance.User = new User();
                    Singleton.Instance.User.Copy(us.User);
                    return 0;
                }
                return 1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Login Exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
                return 2;
            }
        }

        public static async Task<bool> Register(string email, string firstname, string lastname, string password)
        {
            try
            {
                var filter = GetFilter();
                HttpClient client = new HttpClient();
                var content = new HttpFormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("firstname", firstname),
                    new KeyValuePair<string, string>("lastname", lastname),
                });
                var r = await client.PostAsync(new Uri(URL + "/sign_up"), content);
                var response = await r.Content.ReadAsInputStreamAsync();
                string jsonMessage;
                jsonMessage = new StreamReader(response.AsStreamForRead()).ReadToEnd();
                System.Diagnostics.Debug.WriteLine(jsonMessage);
                if (jsonMessage != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Register Exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        /* Run */

        public static async Task<ListRuns> GetRuns()
        {
            var filter = GetFilter();

            var client = new HttpClient(filter);
            var r = await client.GetAsync(new Uri(URL + "/runs?user_token=" + Singleton.Instance.User.token
                + "&user_email=" + Singleton.Instance.User.email));
            var response = await r.Content.ReadAsInputStreamAsync();
            string jsonMessage;
            jsonMessage = new StreamReader(response.AsStreamForRead()).ReadToEnd();
            System.Diagnostics.Debug.WriteLine(jsonMessage);
            ListRuns lr = JsonConvert.DeserializeObject<ListRuns>(jsonMessage);
            return lr;
        }

        public static async Task AddRun(double distance, List<Coordinate> lco)
        {
            try
            {
                string url = URL + "/runs?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                DateTime dt = DateTime.Now;
//                var stringList = lco.OfType<Coord>();
                var combined = string.Join(", ", lco);
                System.Diagnostics.Debug.WriteLine(combined);
                string serialize = JsonConvert.SerializeObject(lco);
                System.Diagnostics.Debug.WriteLine("LCO ===== ===== ===== ===== " + serialize);
               var result = await url
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .PostJsonAsync(new
                    {
                        coordinates = serialize,
                        max_speed = 0,
                        total_distance = distance,
                        total_time = Singleton.Instance.time,
                        started_at = dt,
                        avg_speed = 0,
                        user_id = Singleton.Instance.User.id
                    });
                System.Diagnostics.Debug.WriteLine("AGROUGRUO");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Add post exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        /* Post */

        public static async Task<ListPost> GetPostToEvent(int e_id)
        {
            try
            {
                string url = URL + "/events/" + e_id + "/posts?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                ListPost result = await url
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .GetJsonAsync<ListPost>();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Get Event exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return null;
        }

        public static async Task AddPostToEvent(int e_id, string message)
        {
            try
            {
                string u = URL + "/posts?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                var result = await u
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .PostJsonAsync(new { event_id = e_id,
                    content = message });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Add post exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static async Task AddPost(string message)
        {
            try
            {
                string u = URL + "/posts?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                var result = await u
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .PostJsonAsync(new { content = message });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Add post exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static async Task EditPost(string message, int id_post)
        {
            try
            {
                string u = URL + "/posts/" + id_post + "?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                var result = await u
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .PutJsonAsync(new {
                        content = message,
                        user_email = Singleton.Instance.User.email
                    });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Edit post exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static async Task DeletePost(int id_post)
        {
            try
            {
                string u = URL + "/posts/" + id_post + "?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                var result = await u
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .DeleteAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Edit post exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static async Task<ListPost> GetPost()
        {
            var filter = GetFilter();
            var client = new HttpClient(filter);
            var r = await client.GetAsync(new Uri(URL+
                "/posts?user_token=" + Singleton.Instance.User.token
                + "&user_email=" + Singleton.Instance.User.email));
            var response = await r.Content.ReadAsInputStreamAsync();
            string jsonMessage;
            jsonMessage = new StreamReader(response.AsStreamForRead()).ReadToEnd();
            ListPost lp = JsonConvert.DeserializeObject<ListPost>(jsonMessage);
            return lp;
        }

        /* Comment */

        public static async Task AddComment(string message, int o_post_id)
        {
            try
            {
                string u = URL + "/comments?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                var result = await u
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .PostJsonAsync(new
                    {
                        content = message,
                        user_token = Singleton.Instance.User.token,
                        user_id = Singleton.Instance.User.id,
                        post_id = o_post_id,
                        user_email = Singleton.Instance.User.email
                    });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Add post exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static async Task EditComment(string message, int id, int cpost_id)
        {
            try
            {
                string u = URL + "/comments/" + id + "?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                var result = await u
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .PutJsonAsync(new
                    {
                        content = message,
                        user_token = Singleton.Instance.User.token,
                        user_id = Singleton.Instance.User.id,
                        post_id = cpost_id,
                        user_email = Singleton.Instance.User.email
                    });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Edit comment exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static async Task<ListComment> GetComments()
        {
            var filter = GetFilter();
            var client = new HttpClient(filter);
            var r = await client.GetAsync(new Uri(URL +
                "/comments?user_token=" + Singleton.Instance.User.token
                + "&user_email=" + Singleton.Instance.User.email));
            var response = await r.Content.ReadAsInputStreamAsync();
            string jsonMessage;
            jsonMessage = new StreamReader(response.AsStreamForRead()).ReadToEnd();
            ListComment lc = JsonConvert.DeserializeObject<ListComment>(jsonMessage);
            return lc;
        }

        public static async Task DeleteComment(int comment_id)
        {
            try
            {
                string u = URL + "/comments/" + comment_id +"?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                var result = await u
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .DeleteAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Add post exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        /* Group */

        public static async Task AddGroup(string _name, string _description, bool _check)
        {
            try
            {
                string url = URL + "/groups?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                var result = await url
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .PostJsonAsync(new
                    {
                        name = _name,
                        private_status = _check //_private 
                    });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Add event exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }


        public static async Task<ListGroup> GetGroups()
        {
            try
            {
                string url = URL + "/groups?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                ListGroup result = await url
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .GetJsonAsync<ListGroup>();
                System.Diagnostics.Debug.WriteLine(result);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Get Event exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return null;
        }

        public static async Task AddPostToGroup(int g_id, string message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Add post to group = "  + g_id);
                System.Diagnostics.Debug.WriteLine("Add post to MESSAGE s= "  + message);
                string u = URL + "/posts?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                var result = await u
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .PostJsonAsync(new
                    {
                        group_id = g_id,
                        content = message
                    });
                System.Diagnostics.Debug.WriteLine("result ==" + result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Add post to group exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static async Task<ListPost> GetPostFromGroup(int e_id)
        {
            try
            {
                string url = URL + "/groups/" + e_id + "/posts?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                ListPost result = await url
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .GetJsonAsync<ListPost>();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Get Post Group exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return null;
        }

        public static async Task DeleteGroup(int group_id)
        {
            try
            {
                string url = URL + "/groups/" + group_id + "?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                var result = await url
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .DeleteAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Delete Event exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        /* Event */

        public static async Task<ListEvent> GetEvents()
        {
            try
            {
                string url = URL + "/events?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                ListEvent result = await url
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .GetJsonAsync<ListEvent>();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Get Event exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return null;
        }

        public static async Task EditEvent(int id_event, string event_name, string event_description, string event_city)
        {
            try
            {
                string u = URL + "/events/" + id_event + "?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                var result = await u
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .PutJsonAsync(new
                    {
                        name = event_name,
                        description = event_description,
                        city = event_city,
                        user_email = Singleton.Instance.User.email
                    });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Edit post exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }


        public static async Task DeleteEvent(int event_id)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("DELETE event");
                System.Diagnostics.Debug.WriteLine(event_id);
                string url = URL + "/events/" + event_id + "?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                var result = await url
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .DeleteAsync();
                System.Diagnostics.Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Delete Event exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static async Task AddEvent(string _name, string _description, string _city, bool _check)
        {
            try
            {
                string url = URL + "/events?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email;
                var result = await url
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .PostJsonAsync(new
                    {
                        name = _name,
                        description = _description,
                        city = _city,
                        distance = 42, //_distance,
                        user_id = Singleton.Instance.User.id,
                        private_status = _check //_private
                    });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Add event exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        
        public static async Task<bool> UserProfile()
        {
            try
            {
                var filter = GetFilter();
                HttpClient client = new HttpClient();
                var r = await client.GetAsync(new Uri(URL + "/users/" + Singleton.Instance.User.id
                    + "?user_token=" + Singleton.Instance.User.token
                    + "&user_email=" + Singleton.Instance.User.email));
                var response = await r.Content.ReadAsInputStreamAsync();
                string jsonMessage;
                jsonMessage = new StreamReader(response.AsStreamForRead()).ReadToEnd();
                Profile us = JsonConvert.DeserializeObject<Profile>(jsonMessage);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Profile Exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        /* Friend */

        public static async Task<bool> SendInviteFriend(int uid)
        {
            try
            {
                string url = URL + "/friendships/?user_email=" + Singleton.Instance.User.email
                    + "&user_token=" + Singleton.Instance.User.token + "&user_id=" + uid;
                var result = await url
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .PostJsonAsync(new
                    {
                        user_id = uid
                    });
                System.Diagnostics.Debug.WriteLine("Send invitation to this user");
                System.Diagnostics.Debug.WriteLine(result);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Send invite friend exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public static async Task<Friendship> GetFriendship()
        {
            try
            {
                string url = URL + "/friendships?user_token=" + Singleton.Instance.User.token
                    + "&user_email=" + Singleton.Instance.User.email;
                var result = await url
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .GetJsonAsync<Friendship>();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Get Friendship exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return null;
        }

        public static async Task AcceptFriend(int request_id, int id)
        {
            try
            {
                string url = URL + "/friendships/" + request_id + "/accept?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email + "&user_id=" + id;
                var result = await url
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .PostJsonAsync(new
                    {
                    });
                return;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Add event exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static async Task RejectFriend(int request_id, int id)
        {
            try
            {
                string url = URL + "/friendships/" + request_id + "/reject?user_token="
                + Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email + "&user_id=" + id;
                var result = await url
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .PostJsonAsync(new
                    {
                    });
                return;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Reject exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static async Task<Notifications> GetNotification()
        {
            try
            {
                string url = URL + "/friendships/notifications?user_token=" + Singleton.Instance.User.token
                    + "&user_email=" + Singleton.Instance.User.email;
                var result = await url
                    .WithHeader("X-User-Email", Singleton.Instance.User.email)
                    .WithHeader("X-User-Token", Singleton.Instance.User.token)
                    .GetJsonAsync<Notifications>();
                System.Diagnostics.Debug.WriteLine("Notification == " + result);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Get Invitations exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return null;
        }

        public static async 
        Task
UpdateProfile(string email, string firstname, string lastname,
                                               string phone, string address)
        {
            try
            {
                var filter = GetFilter();
                HttpClient client = new HttpClient();
                var content = new HttpFormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("email", email),
                        new KeyValuePair<string, string>("firstname", firstname),
                        new KeyValuePair<string, string>("lastname", lastname),
                        new KeyValuePair<string, string>("address", address),
                        new KeyValuePair<string, string>("phone", phone),
                });
                var r = await client.PutAsync(new Uri(URL + "/users/"
                    + Singleton.Instance.User.id + "?user_token=" +
                    Singleton.Instance.User.token + "&user_email=" + Singleton.Instance.User.email),
                    content);
                var response = await r.Content.ReadAsInputStreamAsync();
                string jsonMessage;
                jsonMessage = new StreamReader(response.AsStreamForRead()).ReadToEnd();
                System.Diagnostics.Debug.WriteLine(jsonMessage);
                Singleton.Instance.User.firstname = firstname;
                Singleton.Instance.User.lastname = lastname;
                Singleton.Instance.User.email = email;
                Singleton.Instance.User.phone = phone;
                Singleton.Instance.User.address = address;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Update exception: ");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

    }
}
 