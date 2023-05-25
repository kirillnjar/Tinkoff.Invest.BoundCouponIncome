using AutoFixture.Xunit2;
using FluentAssertions;
using Tinkoff.Invest.BoundCouponIncome.Core.Common;
using Xunit;

namespace Tinkoff.Invest.BoundCouponIncome.Test.Core.Common;

public class DateIntervalTest
{
    [Theory, AutoData]
    public void Constructor_WhenToBiggerThanFrom_ShouldEqualExpected(DateTimeOffset timestamp, TimeSpan offset)
    {
        // Arrange
        var from = timestamp;
        var to = timestamp + offset;
        
        // Act
        var result = new DateInterval(from, to);
        
        // Assert
        result.From.Should().Be(from);
        result.To.Should().Be(to);
    }
    
    [Theory, AutoData]
    public void Constructor_WhenFromBiggerThanTo_ShouldThrowArgumentException(DateTimeOffset timestamp, TimeSpan offset)
    {
        // Arrange
        var from = timestamp;
        var to = timestamp - offset;
        
        // Act
        var act = () => new DateInterval(from, to);
        
        // Assert
        act.Should().Throw<ArgumentException>();
    }
}