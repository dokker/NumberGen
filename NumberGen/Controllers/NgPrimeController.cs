using Microsoft.AspNetCore.Mvc;
using NumberGen.Data;
using Microsoft.EntityFrameworkCore;
using NumberGen.Model;
using NumberGen.Requests;
using NumberGen.Responses;

namespace NumberGen.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NgPrimeController : Controller
{
    private readonly NumberGenDbContext _dbContext;

    public NgPrimeController(NumberGenDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    // TODO: rewrite to async
    [HttpGet]
    [Route("lastprime")]
    public async Task<NgPrimeResponse> GetLastPrime()
    {
        
        var ngPrime = await _dbContext.NgPrimes.OrderByDescending(prime => prime.CreatedAt).FirstOrDefaultAsync();
        if (ngPrime == null)
        {
            throw new ArgumentNullException(nameof(ngPrime));
        }

        return new NgPrimeResponse()
        {
            Number = ngPrime.Number,
            CreatedAt = ngPrime.CreatedAt,
            GenerationTime = ngPrime.GenerationTime
        };
    }

    // TODO: rewrite to async
    [HttpGet]
    [Route("generatedprimes")]
    public async Task<NgPrimeListResponse> GetGeneratedPrimes([FromQuery] PagedRequest request)
    {
        IQueryable<NgPrime> query = _dbContext.NgPrimes;
        var pagedQuery = await query.Skip(request.PageSize * (request.Page - 1))
            .Take(request.PageSize).Select(item => new NgPrimeResponse
            {
                Number = item.Number,
                CreatedAt = item.CreatedAt,
                GenerationTime = item.GenerationTime
            }).ToListAsync();

        return new NgPrimeListResponse()
        {
            Primes = pagedQuery,
            PageSize = request.PageSize,
            Page = request.Page
        };
    }
}