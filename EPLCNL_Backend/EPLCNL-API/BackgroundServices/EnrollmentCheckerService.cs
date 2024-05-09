using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.EnrollmentsService;

namespace EPLCNL_API.BackgroundServices
{
    public class EnrollmentCheckerService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public EnrollmentCheckerService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Check enrollments and update wallet here
                using (var scope = _services.CreateScope())
                {
                    var enrollmentService = scope.ServiceProvider.GetRequiredService<IEnrollmentService>();
                    await enrollmentService.CheckAndProcessEnrollmentsAsync();
                }

                // Wait for 60 seconds before next check
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    }

}
