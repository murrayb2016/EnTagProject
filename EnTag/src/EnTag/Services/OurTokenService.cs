using EnTag.Infrastructure;
using EnTag.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnTag.Services
{
    public class OurTokenService
    {
        private OurTokenRepository _otRepo;
        public OurTokenService(OurTokenRepository otr)
        {
            _otRepo = otr;
        }

        public OurToken getTwitter()
        {
            return (from t in _otRepo.getTwitter()
                    select t).FirstOrDefault();
        }

        public OurToken getYoutube()
        {
            return (from t in _otRepo.getYoutube()
                    select t).FirstOrDefault();
        }

        public OurToken getTwitch()
        {
            return (from t in _otRepo.getTwitch()
                    select t).FirstOrDefault();
        }
    }
}
