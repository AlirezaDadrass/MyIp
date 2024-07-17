using Microsoft.AspNetCore.Mvc;

namespace ShowIp.Controllers
{
    [ApiController]
    [Route("api/ip")]
    public class HomeController : ControllerBase
    {
        private string _ip = "";

        [HttpGet]
        public string Get()
        {
            return _ip;
        }
        [HttpPut]
        public string Put(string ip)
        {
            _ip = ip;
            return ip;
        }
    }
}
