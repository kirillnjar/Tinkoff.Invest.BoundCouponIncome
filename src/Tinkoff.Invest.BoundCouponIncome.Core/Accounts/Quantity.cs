using Dawn;
using ValueOf;

namespace Tinkoff.Invest.BoundCouponIncome.Core.Accounts;

public class Quantity : ValueOf<int, Quantity>
{
    protected override void Validate()
    {
        Guard.Argument(Value).NotNegative(arg => $"Quantity '{arg}' not valid. It must be not negative");
    }
}