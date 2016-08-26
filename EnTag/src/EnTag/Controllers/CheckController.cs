using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnTag.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EnTag.Controllers
{
    [Route("api/[controller]")]
    public class CheckController : Controller
    {
        private CheckService _cServ;
        public CheckController(CheckService cs)
        {
            _cServ = cs;
        }

        [HttpGet]
        public bool checkUser()
        {
            return User.Identity.IsAuthenticated;
        }

        [HttpGet("twitter")]
        public bool CheckTwitter()
        {
            var check = _cServ.CheckCred(User.Identity.Name, "Twitter");

            return check;
        }

        [HttpGet("youtube")]
        public bool CheckYouTube()
        {
            var check = _cServ.CheckCred(User.Identity.Name, "YouTube");

            return check;
        }

        [HttpGet("twitch")]
        public bool CheckTwitch()
        {
            bool check = _cServ.CheckCred(User.Identity.Name, "Twitch");

            return check;
        }

        [HttpGet("spotify")]
        public bool CheckSpotify()
        {
            var check = _cServ.CheckCred(User.Identity.Name, "Spotify");

            return check;
        }

    }
}
