namespace NumberGen.Responses;

public class NgPrimeListResponse
{
    public List<NgPrimeResponse> Primes { get; set; }
    public int PageNumber { get; set; }
    public int Count { get; set; }
}