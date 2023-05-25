using AutoFixture.Xunit2;
using FluentAssertions;
using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Xunit;

namespace Tinkoff.Invest.BoundCouponIncome.Test.Core.Bonds;

public class FigiTest
{
    [Theory, AutoData]
    public void From_WhenFigiNotEmptyAndNotNull_ShouldSuccess(string figi)
    {
        // Arrange
        
        // Act
        var result = Figi.From(figi);
        
        // Assert
        result.Value.Should().Be(figi);
    }
    
#nullable disable
    [Theory]
    [InlineData("")]
    [InlineData((string)null)]
    public void From_WhenFigiNullOrEmpty_ShouldThrowArgumentException(string rawFigi)
    {
        // Arrange
        
        // Act
        var act = () => Figi.From(rawFigi);
        
        // Assert
        act.Should().Throw<ArgumentException>();
    }
#nullable enable
}