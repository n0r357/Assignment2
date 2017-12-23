using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Jimmy_Waern_Assignment2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Jimmy_Waern_Assignment2.Controllers
{
    [Route("api/news")]
    public class NewsController : Controller
    {
        [HttpGet, Route("open")]
        public IActionResult OpenNews()
        {
            return Ok("Open News");
        }

        [Authorize(Policy = "HiddenPolicy")]
        [HttpGet, Route("hidden")]
        public IActionResult HiddenNews()
        {
            return Ok("Hidden News");
        }

        [Authorize(Policy = "AdultPolicy")]
        [Authorize(Policy = "HiddenPolicy")]
        [HttpGet, Route("adult")]
        public IActionResult AdultNews()
        {
            return Ok("Adult News");
        }

        [Authorize(Policy = "SportsPolicy")]
        [HttpGet, Route("sport")]
        public IActionResult PublishSport()
        {
            return Ok("Publish: Sport");
        }

        [Authorize(Policy = "CulturePolicy")]
        [HttpGet, Route("culture")]
        public IActionResult PublishCulture()
        {
            return Ok("Publish: Culture");
        }
    }
}
