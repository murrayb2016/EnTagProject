using EnTag.Infrastructure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EnTag.Services
{
    public class SpotifyPlaylistService
    {

        private TokenRepository _tRepo;
        public SpotifyPlaylistService(TokenRepository tr)
        {
            _tRepo = tr;
        }

        public dynamic GetPlaylist(string username)
        {

            var token = _tRepo.GetSpotifyToken(username);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://api.spotify.com/v1/browse/featured-playlists"));
            request.Method = "GET";
            request.Headers.Add("authorization", "Bearer " + token);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stremRead = new StreamReader(response.GetResponseStream());
            string result = stremRead.ReadToEnd();

            dynamic qatch = JObject.Parse(result);

            return qatch;

        }
    }
}
