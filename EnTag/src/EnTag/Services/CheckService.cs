using EnTag.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnTag.Services
{
    public class CheckService
    {
        private TokenRepository _tRepo;
        public CheckService(TokenRepository tr)
        {
            _tRepo = tr;
        }

        public bool CheckCred(string username, string service)
        {
            var toCheck = (from t in _tRepo.GetCreds(username)
                           where t.Service == service
                           select t.Token).FirstOrDefault();
            if(toCheck != null)
            {
                return true;
            }

            return false;
        }
    }
}
