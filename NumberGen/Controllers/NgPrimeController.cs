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

    [HttpGet]
    [Route("lastprime")]
    public NgPrimeResponse GetLastPrime()
    {
        
        var ngPrime = _dbContext.NgPrimes.OrderByDescending(prime => prime.CreatedAt).FirstOrDefault();
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

    [HttpGet]
    [Route("generatedprimes")]
    public NgPrimeListResponse GetGeneratedPrimes([FromQuery] PagedRequest request)
    {
        
        return null;
    }
}