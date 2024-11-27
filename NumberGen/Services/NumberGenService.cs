using NumberGen.Data;

namespace NumberGen.Services;

public class NumberGenService : BackgroundService
{
    private readonly NumberGenDbContext _dbContext;
    private readonly ILogger<NumberGenService> _logger;

    public NumberGenService(NumberGenDbContext dbContext, ILogger<NumberGenService> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("starting number gen service");
        throw new NotImplementedException();
        // TODO: dbcontext singletonban
        // TODO: itt kell generálni a primszámot és DB-be menteni
    }
}