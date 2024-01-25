using DocHub.Core.Enums.Appointments;
using DocHub.Infrastructure.DbContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DocHub.Infrastructure.BackgroundServices;

public class AppointmentsCleanUpBackgroundService : BackgroundService
{
    private readonly IServiceProvider _provider;

    public AppointmentsCleanUpBackgroundService(IServiceProvider provider)
    {
        _provider = provider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CleanUp();
            await Task.Delay(TimeSpan.FromHours(12), stoppingToken);
        }
    }

    private async Task CleanUp()
    {
        using (var scope = _provider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var expiredAppointments = db.Appointments
                .Where(a => a.End < DateTime.Now && a.State == State.Available.ToString());
            db.Appointments.RemoveRange(expiredAppointments);
            await db.SaveChangesAsync();

        }
    }
}