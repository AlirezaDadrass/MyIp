using MyIp.Controllers;
using Newtonsoft.Json;

namespace MyIp.Services
{
    public class TimedHostedService : BackgroundService
    {
        public TimedHostedService(IpService ipService)
        {
            IpService = ipService;
        }

        public IpService IpService { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(30));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    var ip = JsonConvert.DeserializeObject<IpResponse>(await IpService.GetIp());

                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Put, $"http://188.213.65.229:5001/api/ip?ip={ip?.Ip}");
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                }
                catch { }
            }
        }
    }
}
