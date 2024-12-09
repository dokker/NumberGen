namespace NumberGen.Responses;

public class NgPrimeResponse
{
    public ulong Number { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public long GenerationTime { get; set; }
}