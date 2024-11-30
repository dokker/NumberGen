using Microsoft.EntityFrameworkCore;
using NumberGen.Data;
using NumberGen.Model;

namespace NumberGen.Services;

public class NumberGenService : BackgroundService
{
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
        using var scope = _serviceProvider.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<NumberGenDbContext>();
        
        _logger.LogInformation("starting number gen service");
        
        var lastPrime = dbContext.NgPrimes.OrderByDescending(n => n.Number).Select(n => n.Number).FirstOrDefault();

        if (lastPrime == default)
        {
            lastPrime = 1;
        }
        
        var nextNumber = lastPrime + 1;
        
        _logger.LogInformation("starting generating prime numbers from last prime: {PrimeNumber}", lastPrime);

        while (!stoppingToken.IsCancellationRequested)
        {
            if (IsPrime(nextNumber, stoppingToken))
            {
                var primeEntity = new NgPrime
                {
                    Number = nextNumber,
                    CreatedAt = DateTime.UtcNow,
                };
                
                dbContext.NgPrimes.Add(primeEntity);
                await dbContext.SaveChangesAsync(stoppingToken);

                _logger.LogInformation("Next Prime number found: {nextNumber}", nextNumber);
            }

            nextNumber++;
        }
        
        _logger.LogInformation("stopping number gen service");
    }
    
    private bool IsPrime(ulong number, CancellationToken stoppingToken)
    {
        switch (number)
        {
            case < 2:
                return false;
            case 2:
                return true;
        }

        if (number % 2 == 0) return false;
        
        var boundary = (ulong)Math.Floor(Math.Sqrt(number));
        
        for (ulong x = 3; x <= boundary; x += 2)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                return false;
            }
            
            if (number % x == 0)
            {
                return false;           
            }
        }
        
        return true;
    }   
}