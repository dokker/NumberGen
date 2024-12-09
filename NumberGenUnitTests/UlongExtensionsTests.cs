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
    
    [Theory]
    [InlineData(1, false)]
    [InlineData(2, true)]
    [InlineData(8, false)]
    [InlineData(11, true)]
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