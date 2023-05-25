using Dawn;
using ValueOf;

namespace Tinkoff.Invest.BoundCouponIncome.Core.Bonds;

public class Figi : ValueOf<string, Figi>
{
    protected override void Validate()
    {
        Guard.Argument(Value).NotEmpty(_ => "Figi must be not empty")
            .NotNull("Figi must has value");
    }
}