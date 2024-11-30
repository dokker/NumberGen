namespace NumberGen.Responses;

public class NgPrimeListResponse
{
    public List<NgPrimeResponse> Primes { get; set; }
    public int PageSize { get; set; }
    public int Page { get; set; }
}