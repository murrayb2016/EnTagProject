using EnTag.Infrastructure;
using EnTag.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnTag.Services
{
    public class TwitchService
    {
        private TokenRepository _tRepo;
        public TwitchService(TokenRepository tr)
        {
            _tRepo = tr;
        }

        public TokenDTO GetCreds(string username)
        {
            return (from t in _tRepo.GetCreds(username)
                    where t.Service == "Twitch"
                    select new TokenDTO()
                    {
                        Token = t.Token
                    }).FirstOrDefault();
        }
    }
}
