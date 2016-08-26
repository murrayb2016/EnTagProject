using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnTag.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Routing;
using Tweetinvi;
using Tweetinvi.Models;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EnTag.Controllers
{
    [Route("[controller]")]
    public class OAuthController : Controller
    {
        private TwitterAuthService _twitter;
        private SpotifyAuthService _spotify;
        private TwitchAuthService _twitch;
        private OurTokenService _otServ;
        public OAuthController(TwitterAuthService t,SpotifyAuthService s, TwitchAuthService tw, OurTokenService ots)
        {
            _twitter = t;
            _spotify = s;
            _twitch = tw;
            _otServ = ots;
        }

        private IAuthenticationContext _authContext;

        [HttpGet("twitter")]
        [Authorize]
        public ActionResult Twitter()
        {
            var redirectUrl = "http://" + Request.Host.Value + "/oauth/twitter/auth";

            _authContext = _twitter.TwitterAuth(redirectUrl);
            
            return new RedirectResult(_authContext.AuthorizationURL);
        }

        [HttpGet("twitter/auth", Name = "TwitterAuth")]
        [Authorize]
        public ActionResult TwitterAuth([FromQuery]string oauth_verifier,[FromQuery] string oauth_token,[FromQuery] string authorization_id)
        {
            

            var test = _twitter.ValidateTwitterAuth(oauth_verifier, authorization_id);

            _twitter.AddItIn(test.AccessToken, test.AccessTokenSecret, "Twitter", User.Identity.Name);
            return Ok();
        }

        [HttpGet("twitch")]
        [Authorize]
        public ActionResult Twitch()
        {
            var redirectUrl = "http://" + Request.Host.Value + "/oauth/twitch/auth";
            var id = _otServ.getTwitch(); //s3n11pgqzcy09gqlz7gdgjloc3vzfav
            var scopes = "user_read%20user_subscriptions";
            var loginUrl = "https://api.twitch.tv/kraken/oauth2/authorize?response_type=code&client_id=" + id.Token + "&redirect_uri=" + redirectUrl + "&scope=" + scopes;

            return Redirect(loginUrl);
        }

        [HttpGet("twitch/auth", Name = "TwitchAuth")]
        [Authorize]
        public ActionResult TwitchAuth([FromQuery]string code)
        {
            var qatch = _twitch.AuthTwitch(code, "http://" + Request.Host.Value + "/oauth/twitch/auth");
            dynamic test = JObject.Parse(qatch);
            _twitch.AddItIn((string)test.access_token, "", "Twitch", User.Identity.Name);
            return Ok();
        }


        [HttpGet("spotify")]
        [Authorize]
        public ActionResult Spotify()
        {
            var redirectUrl = "http://" + Request.Host.Value + "/oauth/spotify/auth";
            var id = "f9777aac06be407cb2a7c45e20e8c5a0";
            var scopes = "user-read-private%20user-library-read%20playlist-read-collaborative";
            var loginUrl = "https://accounts.spotify.com/authorize/?client_id=" + id + "&response_type=code&redirect_uri=" + redirectUrl + "&scope=" + scopes + "&state=34fFs29kd09";
            return Redirect(loginUrl);
        }

        [HttpGet("spotify/auth", Name = "SpotifyAuth")]
        [Authorize]
        public ActionResult SpotifyAuth([FromQuery]string code)
        {
            var qatch = _spotify.AuthSpotify(code, "http://" + Request.Host.Value + "/oauth/spotify/auth");
            dynamic test = JObject.Parse(qatch);
            _spotify.PostSpotifyUsername(User.Identity.Name, (string)test.access_token);
            return Ok();
        }

    }
}
