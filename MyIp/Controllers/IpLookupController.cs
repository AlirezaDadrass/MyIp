using Microsoft.AspNetCore.Mvc;
using MyIp.Services;
using Newtonsoft.Json;

namespace MyIp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpLookupController(IpService ipService) : ControllerBase
    {

        [HttpGet(Name = "GetIp")]
        public async Task<string?> Get()
        {
            var ip = JsonConvert.DeserializeObject<IpResponse>(await ipService.GetIp());
            return ip?.Ip;
        }
    }
    class IpResponse
    {
        public string? Ip { get; set; }
    }
}
