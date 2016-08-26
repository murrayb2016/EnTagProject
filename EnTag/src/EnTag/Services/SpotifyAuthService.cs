using EnTag.Infrastructure;
using EnTag.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnTag.Services
{
    public class SpotifyAuthService
    {
        private TokenRepository _tRepo;
        public SpotifyAuthService(TokenRepository tr)
        {
            _tRepo = tr;
        }

        public string AuthSpotify(string code, string url)
        {
            var redirectUrl = url;
            var id = "f9777aac06be407cb2a7c45e20e8c5a0";
            var secret = "1c92293bb77b48d7b91012e1290f37bf";
           
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "client_id=" + id;
            postData += ("&client_secret=" + secret);
            postData += ("&grant_type=authorization_code");
            postData += ("&redirect_uri=" + redirectUrl);
            postData += ("&code=" + code);
            byte[] data = encoding.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://accounts.spotify.com/api/token"));
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

        public void PostSpotifyUsername(string username, string token)
        {

            ExternalToken dbToken = new ExternalToken()
            {
                Service = "Spotify",
                Token = token,
                Secret = "",
                UserId = _tRepo.GetUserId(username)
            };

            _tRepo.AddSpotifyUserName(dbToken);

        }


    }
}
