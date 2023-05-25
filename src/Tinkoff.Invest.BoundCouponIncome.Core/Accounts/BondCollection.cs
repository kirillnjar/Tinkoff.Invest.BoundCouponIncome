using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Tinkoff.Invest.BoundCouponIncome.Core.Repayments;

namespace Tinkoff.Invest.BoundCouponIncome.Core.Accounts;

public record BondCollection(Bond Bond, Quantity Quantity)
{
    public IReadOnlyCollection<Repayment> CombineCouponRepaymentsByQuantity() =>
        Bond.GetCouponRepayments.Select(r => r with {Amount = Amount.From(r.Amount.Value * Quantity.Value)})
            .ToList();
    
    public Repayment CombineMaturityRepaymentsByQuantity()
    {
        var maturityRepayment = Bond.GetMaturityRepayment;
        return maturityRepayment with {Amount = Amount.From(maturityRepayment.Amount.Value * Quantity.Value)};
    }
}