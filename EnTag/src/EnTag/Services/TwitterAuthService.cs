
using EnTag.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace EnTag.Services
{
    public class TwitterAuthService
    {
        private IHttpContextAccessor _context;
        private TokenRepository _tRepo;
        public TwitterAuthService(IHttpContextAccessor context, TokenRepository tr)
        {
            _context = context;
            _tRepo = tr;
        }


        // Step 1 : Redirect user to go on Twitter.com to authenticate
        public IAuthenticationContext TwitterAuth(string redirectUrl)
        {
            var appCreds = new ConsumerCredentials("pH8Qoql342mGPXHNY0Wnk3LZX", "kzIFhr3VaTW9yZJFh4WVvltFA45RXPjMD8IvxkcA2xvbwG5Lsk");

            // Specify the url you want the user to be redirected to
            var test = AuthFlow.InitAuthentication(appCreds, redirectUrl);

            return test;
        }

        public ITwitterCredentials ValidateTwitterAuth(string verifierCode, string authorization_id)
        {

            var userCreds = AuthFlow.CreateCredentialsFromVerifierCode(verifierCode, authorization_id);

            return userCreds;

        }

        public void AddItIn(string token, string secret, string service, string username)
        {
            _tRepo.AddItIn(token, secret, service, username);
        }

    }
}
