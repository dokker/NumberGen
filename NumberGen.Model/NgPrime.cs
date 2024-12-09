using System.Numerics;

namespace NumberGen.Model;

public class NgPrime
{
    public ulong Number { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public long GenerationTime { get; set; }
}