using AutoFixture.Xunit2;
using FluentAssertions;
using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;
using Xunit;

namespace Tinkoff.Invest.BoundCouponIncome.Test.Core.Accounts;

public class IdTest
{
    [Theory, AutoData]
    public void From_WhenIdPositive_ShouldSuccess(long id)
    {
        // Arrange
        
        // Act
        var result = Id.From(id);
        
        // Assert
        result.Value.Should().Be(id);
    }
    
    [Theory, AutoData]
    public void From_WhenIdNegative_ShouldThrowArgumentException(long id)
    {
        // Arrange
        
        // Act
        var act = () => Id.From(-id);
        
        // Assert
        act.Should().Throw<ArgumentException>();
    }
    
    
    [Fact]
    public void From_WhenIdIsZero_ShouldThrowArgumentException()
    {
        // Arrange
        
        // Act
        var act = () => Id.From(0);
        
        // Assert
        act.Should().Throw<ArgumentException>();
    }
}