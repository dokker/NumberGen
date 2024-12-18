using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NumberGen.Data;
using NumberGen.Extensions;
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

        var stopwatch = new Stopwatch();
        while (!stoppingToken.IsCancellationRequested)
        {
            stopwatch.Restart();
            var isPrime = nextNumber.IsPrime(stoppingToken);
            stopwatch.Stop();
            var generationTime = stopwatch.ElapsedTicks;
            
            if (isPrime)
            {
                var primeEntity = new NgPrime
                {
                    Number = nextNumber,
                    CreatedAt = DateTime.UtcNow,
                    GenerationTime = generationTime
                };
                
                dbContext.NgPrimes.Add(primeEntity);
                await dbContext.SaveChangesAsync(stoppingToken);
                _logger.LogInformation("Next Prime number found: {nextNumber}. Generation time: {generationTime}",
                    nextNumber, generationTime);
            }

            nextNumber++;
            // only for testing purposes
            await Task.Delay(1000, stoppingToken);
        }
        
        _logger.LogInformation("stopping number gen service");
    }
}