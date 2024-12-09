namespace NumberGen.Extensions;

public static class UlongExtensions
{
    public static bool IsPrime(this ulong number, CancellationToken stoppingToken = default)
    {
        switch (number)
        {
            case < 2:
                return false;
            case 2:
                return true;
        }

        if (number % 2 == 0) return false;
        
        var boundary = (ulong)Math.Floor(Math.Sqrt(number));
        
        for (ulong x = 3; x <= boundary; x += 2)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                return false;
            }
            
            if (number % x == 0)
            {
                return false;           
            }
        }
        
        return true;
    }
}