using System.Numerics;

namespace NumberGen.Models;

public class NgPrime
{
    public ulong Number { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public int GenerationTime { get; set; }
}