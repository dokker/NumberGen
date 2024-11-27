namespace NumberGen.Responses;

public class NgPrimeResponse
{
    public ulong Number { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public int GenerationTime { get; set; }
}