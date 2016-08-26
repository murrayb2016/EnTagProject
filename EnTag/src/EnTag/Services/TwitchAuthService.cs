using EnTag.Infrastructure;
using EnTag.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnTag.Services
{
    public class TwitchAuthService
    {
        private TokenRepository _tRepo;
        public TwitchAuthService(TokenRepository tr)
        {
            _tRepo = tr;
        }

        public string AuthTwitch(string code, string url)
        {
            var redirectUrl = url;
            var id = "s3n11pgqzcy09gqlz7gdgjloc3vzfav";
            var secret = "92u0j0kx8u1l92u6yffyyafju245s8a";
            //var twitchPost = "https://api.twitch.tv/kraken/oauth2/token?client_id=" + id + "&client_secret=" + secret + "&grant_type=authorization_code&redirect_uri=" + redirectUrl + "&code=" + code;

            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "client_id=" + id;
            postData += ("&client_secret=" + secret);
            postData += ("&grant_type=authorization_code");
            postData += ("&redirect_uri=" + redirectUrl);
            postData += ("&code=" + code);
            byte[] data = encoding.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://api.twitch.tv/kraken/oauth2/token"));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            var strem = request.GetRequestStream();
            strem.Write(data, 0, data.Length);
            strem.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stremRead = new StreamReader(response.GetResponseStream());
            string result = stremRead.ReadToEnd();

            

            return result;
        }

        public void AddItIn(string token, string secret, string service, string username)
        {
            _tRepo.AddItIn(token, secret, service, username);
        }
    }
}
