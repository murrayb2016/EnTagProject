using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tweetinvi.Models;
using Tweetinvi;
using EnTag.Services;
using EnTag.Services.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EnTag.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private TokenService _tokenService;
        private SpotifyPlaylistService _spotifyPlaylist;
        public TestController(TokenService ts,SpotifyPlaylistService sps) {
            _tokenService = ts;
            _spotifyPlaylist = sps;
        }

        // GET: api/values
        [HttpGet]
        [Authorize]
        public IEnumerable<ITweet> GetHomeTest()
        {
            return _tokenService.GetHomeTest(User.Identity.Name);
        }

        [HttpGet("youtube/username")]
        public TokenDTO GetYouTubeUsername() {

            return _tokenService.GetYouTubeUsername(User.Identity.Name);
        } 



        [HttpPost]
        [Authorize]
        public IActionResult PostTweetTest([FromBody]string myTweet)
        {

            _tokenService.PostTweetTest(myTweet);

            return Ok();
        }

        [HttpPost("youtube/username")]
        [Authorize]
        public IActionResult PostYoutubeUsername([FromBody] string username) {

            _tokenService.PostYoutubeUsername(User.Identity.Name, username);

            return Ok();
        }

        [HttpGet("spotify/playlist")]
        public dynamic GetSpotifyPlaylist()
        {

            var result = _spotifyPlaylist.GetPlaylist(User.Identity.Name);

            return result;
        }


    }
}
