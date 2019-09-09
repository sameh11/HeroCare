using HeroCare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HeroCare.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("you are on home page");
            //return View();
        }

        //[HttpGet]
        public IActionResult Privacy()
        {
            return Ok("no privacey is setup yet");
            //return View();
        }

        //[HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return Ok(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
