namespace MyIp.Services
{
    public class TimedHostedService(IpService ipService) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(30));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    var ip = await ipService.GetIp();

                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Put, $"https://188.213.65.229:50000/api/ip?ip={ip}");
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                }
                catch { }
            }
        }
    }
}
