using Microsoft.EntityFrameworkCore;
using NumberGen.Data;
using NumberGen.Model;

namespace NumberGen.Services;

public class NumberGenService : BackgroundService
{
    //private readonly NumberGenDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<NumberGenService> _logger;

    public NumberGenService(IServiceProvider serviceProvider, ILogger<NumberGenService> logger)
    {
        //_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _serviceProvider = serviceProvider;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("starting number gen service");

        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<NumberGenDbContext>();
                var lastPrime = dbContext.NgPrimes.OrderByDescending(n => n.Number).Select(n => n.Number).FirstOrDefault();
                
                if (lastPrime == 0)
                {
                    lastPrime = 1;
                }
                var nextNumber = lastPrime + 1;
                while (true)
                {
                    if (IsPrime(nextNumber))
                    {
                        var primeEntity = new NgPrime
                        {
                            Number = nextNumber,
                            CreatedAt = DateTime.UtcNow,
                        };
                        
                        // TODO: store nextNumber value in DB
                        dbContext.NgPrimes.Add(primeEntity);
                        dbContext.SaveChanges();
                        // TODO: send nextNumber in response

                        _logger.LogInformation($"Next Prime number found: {nextNumber}");
                    }

                    nextNumber++;
                }
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
        //throw new NotImplementedException();
    }
    
    private bool IsPrime(ulong number)
    {
        if (number < 2) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;
        
        var boundary = (ulong)Math.Floor(Math.Sqrt(number));
        for (ulong x = 3; x <= boundary; x += 2)
        {
            if (number % x == 0)
            {
                return false;           
            }
        }
        return true;
    }
}