using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using EnTag.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EnTag.Controllers
{
    [Route("api/[controller]")]
    public class TwitchController : Controller
    {
        private TwitchService _twitch;
        public TwitchController(TwitchService ts)
        {
            _twitch = ts;
        }
        //GET /api/twitch/follows/username
        [HttpGet("follows/{username}")]
        [Authorize]
        public string GetFollows(string username)
        {
            //ASCIIEncoding encoding = new ASCIIEncoding();
            //string postData = "/users/" + username;
            //postData += "/follows/channels";
            //byte[] data = encoding.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://api.twitch.tv/kraken/users/" + username + "/follows/channels"));
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stremRead = new StreamReader(response.GetResponseStream());
            string result = stremRead.ReadToEnd();

            return result;
        }

        [HttpGet("follows/live")]
        [Authorize]
        public string GetLive()
        {
            var creds = _twitch.GetCreds(User.Identity.Name);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://api.twitch.tv/kraken/streams/followed?stream_type=live"));
            request.Method = "GET";
            request.Headers.Add("Authorization", "OAuth " + creds.Token);
            string result;

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader stremRead = new StreamReader(response.GetResponseStream());
                return result = stremRead.ReadToEnd();
            }
            catch (WebException ex) {
                return null;
            }
            
        }

        [HttpGet("username")]
        [Authorize]
        public string GetName()
        {
            var creds = _twitch.GetCreds(User.Identity.Name);

            //ASCIIEncoding encoding = new ASCIIEncoding();
            //string postData = "oauth_token=" + id;
            //byte[] data = encoding.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://api.twitch.tv/kraken?oauth_token=" + creds.Token));
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stremRead = new StreamReader(response.GetResponseStream());
            string result = stremRead.ReadToEnd();

            dynamic qatch = JObject.Parse(result);

            return qatch.token.user_name;
        }
    }
}
