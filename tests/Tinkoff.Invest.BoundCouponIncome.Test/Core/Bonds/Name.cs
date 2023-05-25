using AutoFixture.Xunit2;
using FluentAssertions;
using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Xunit;

namespace Tinkoff.Invest.BoundCouponIncome.Test.Core.Bonds;

public class NameTest
{
    [Theory, AutoData]
    public void From_WhenNameNotEmptyAndNotNull_ShouldSuccess(string name)
    {
        // Arrange
        
        // Act
        var result = Name.From(name);
        
        // Assert
        result.Value.Should().Be(name);
    }
    
#nullable disable
    [Theory]
    [InlineData("")]
    [InlineData((string)null)]
    public void From_WhenNameNullOrEmpty_ShouldThrowArgumentException(string name)
    {
        // Arrange
        
        // Act
        var act = () => Name.From(name);
        
        // Assert
        act.Should().Throw<ArgumentException>();
    }
#nullable enable
}