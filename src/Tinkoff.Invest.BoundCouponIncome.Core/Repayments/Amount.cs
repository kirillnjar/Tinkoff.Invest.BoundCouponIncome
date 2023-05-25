using Dawn;
using ValueOf;

namespace Tinkoff.Invest.BoundCouponIncome.Core.Repayments;

public class Amount : ValueOf<decimal, Amount>
{
    protected override void Validate()
    {
        Guard.Argument(Value).NotNegative(_ => "Amount must be not negative");
    }
}