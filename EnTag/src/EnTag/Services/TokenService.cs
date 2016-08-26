using EnTag.Infrastructure;
using EnTag.Models;
using EnTag.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Internal.Networking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Credentials.Models;
using Tweetinvi.Models;


namespace EnTag.Services
{
    public class TokenService
    {
        private TokenRepository _tRepo;
        private OurTokenService _otServ;
        private IHttpContextAccessor _context;
        public TokenService(TokenRepository tr, OurTokenService ots, IHttpContextAccessor context)
        {
            _tRepo = tr;
            _otServ = ots;
            _context = context;
        }

        public TokenDTO GetTwitterCreds(string username)
        {
            return (from t in _tRepo.GetCreds(username)
                    where t.Service == "Twitter"
                    select new TokenDTO()
                    {
                        Token = t.Token,
                        Secret = t.Secret
                    }).FirstOrDefault();
        }


        public TokenDTO GetYouTubeUsername(string username)
        {
            return (from t in _tRepo.GetCreds(username)
                    where t.Service == "YouTube"
                    select new TokenDTO()
                    {
                        Token = t.Token,
                        Secret = "",
                    }).FirstOrDefault();
        }



        public IEnumerable<ITweet> GetHomeTest(string username)
        {
            var test = this.GetTwitterCreds(username);
            if (test != null)
            {
                var test2 = _otServ.getTwitter();
                Auth.SetUserCredentials(test2.Token, test2.Secret, test.Token, test.Secret);

                var tweets = Timeline.GetHomeTimeline();

                return tweets;
            }
            return Enumerable.Empty<ITweet>();
        }

        public void PostTweetTest(string myTweet) {

            var firstTweet = Tweet.PublishTweet(myTweet);
        }

        public void PostYoutubeUsername(string username,string ytUsername) {

            ExternalToken dbToken = new ExternalToken()
            {
                Service = "YouTube",
                Token = ytUsername,
                Secret = "",
                UserId = _tRepo.GetUserId(username)
            };

            _tRepo.AddYoutubeUserName(dbToken);

        }




    }
}
