using Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace Infastructure.Workers;

public class AutomaticDeleterProducts : IJob
{
    private readonly IServiceScopeFactory _scopeFactory;
    public AutomaticDeleterProducts(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        using var scope = _scopeFactory.CreateScope(); 
        var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var products = await _context.Advertisements
            .Where(x => x.Status == 1 && x.DisActiveDate !=null)
            .ToListAsync();
        foreach (var product in products)
        {
             _context.Advertisements.Remove(product);
        }
        await _context.SaveChangesAsync();
    }
}