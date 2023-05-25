using AutoFixture.Xunit2;
using FluentAssertions;
using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Xunit;

namespace Tinkoff.Invest.BoundCouponIncome.Test.Core.Bonds;

public class BondTest
{
    
    [Theory, AutoData]
    public void GetCouponRepaymentsShouldBeEquivalentTo(Bond bond)
    {
        // Arrange
        var expected = bond.Coupons.Select(x => x.Repayment).ToList();
        
        // Act
        var result = bond.GetCouponRepayments;
        
        // Assert
        result.Should().BeEquivalentTo(expected);
    }
    
    [Theory, AutoData]
    public void GetMaturityRepayment_ShouldEqualExpected(Bond bond)
    {
        // Arrange
        var expectedPaymentDate = bond.MaturityDate;
        var expectedAmount = bond.Nominal;
        
        // Act
        var result = bond.GetMaturityRepayment;
        
        // Assert
        result.Amount.Should().Be(expectedAmount);
        result.Date.Should().Be(expectedPaymentDate);
    }
}