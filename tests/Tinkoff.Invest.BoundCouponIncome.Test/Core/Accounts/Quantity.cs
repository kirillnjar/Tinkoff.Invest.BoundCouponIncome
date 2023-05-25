using AutoFixture.Xunit2;
using FluentAssertions;
using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;
using Xunit;

namespace Tinkoff.Invest.BoundCouponIncome.Test.Core.Accounts;

public class QuantityTest
{
    [Theory, AutoData]
    public void From_WhenQuantityPositive_ShouldSuccess(int quantity)
    {
        // Arrange
        
        // Act
        var result = Quantity.From(quantity);
        
        // Assert
        result.Value.Should().Be(quantity);
    }
    
    [Theory, AutoData]
    public void From_WhenQuantityNegative_ShouldThrowArgumentException(int quantity)
    {
        // Arrange
        
        // Act
        var act = () => Quantity.From(-quantity);
        
        // Assert
        act.Should().Throw<ArgumentException>();
    }
    
    
    [Fact]
    public void From_WhenQuantityIsZero_ShouldSuccess()
    {
        // Arrange
        
        // Act
        var result = Quantity.From(0);
        
        // Assert
        result.Value.Should().Be(0);
    }
}