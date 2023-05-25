using AutoFixture.Xunit2;
using FluentAssertions;
using Tinkoff.Invest.BoundCouponIncome.Core.Repayments;
using Xunit;

namespace Tinkoff.Invest.BoundCouponIncome.Test.Core.Repayments;

public class AmountTest
{
    [Theory, AutoData]
    public void From_WhenIdPositive_ShouldSuccess(decimal amount)
    {
        // Arrange
        
        // Act
        var result = Amount.From(amount);
        
        // Assert
        result.Value.Should().Be(amount);
    }
    
    [Theory, AutoData]
    public void From_WhenIdNegative_ShouldThrowArgumentException(decimal amount)
    {
        // Arrange
        
        // Act
        var act = () => Amount.From(-amount);
        
        // Assert
        act.Should().Throw<ArgumentException>();
    }
    
    
    [Fact]
    public void From_WhenIdIsZero_ShouldSuccess()
    {
        // Arrange
        
        // Act
        var result = Amount.From(0);
        
        // Assert
        result.Value.Should().Be(0);
    }
}