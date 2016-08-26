using EnTag.Data;
using EnTag.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnTag.Infrastructure
{
    public class OurTokenRepository
    {
        private ApplicationDbContext _db;
        public OurTokenRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<OurToken> getTwitter()
        {
            return from t in _db.OurTokens
                   where t.Service == "Twitter"
                   select t;
        }

        public IQueryable<OurToken> getYoutube()
        {
            return from t in _db.OurTokens
                   where t.Service == "YouTube"
                   select t;
        }

        public IQueryable<OurToken> getTwitch()
        {
            return from t in _db.OurTokens
                   where t.Service == "Twitch"
                   select t;
        }
    }
}
