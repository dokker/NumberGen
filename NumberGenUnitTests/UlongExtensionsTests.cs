using System.Threading;
using Bogus;
using FluentAssertions;
using NumberGen.Extensions;

namespace NumberGenUnitTests;

public class UlongExtensionsTests
{
    private readonly Faker _faker;

    public UlongExtensionsTests()
    {
        _faker = new Faker();
    }
    
    // TODO: write a couple of different cases
    [Theory]
    [InlineData(2, true)]
    public void IsPrimeShouldDeterminePrimeNumber(ulong number, bool expectedResult)
    {
        // arrange act assert
        number.IsPrime().Should().Be(expectedResult);
    }

    [Fact]
    public void IsPrimeShouldBeFalseWhenCancellationTokenCancelledAndNumberBiggerThanTwoAndNumberIsOdd()
    {
        // arrange
        var number = _faker.Random.ULong(3);
        var tokenSource = new CancellationTokenSource();
        
        // act
        tokenSource.Cancel();
        var result = number.IsPrime(tokenSource.Token);
        
        // assert
        result.Should().BeFalse();
    }
    
}