using Dawn;
using ValueOf;

namespace Tinkoff.Invest.BoundCouponIncome.Core.Bonds;

public class Name : ValueOf<string, Name>
{
    protected override void Validate()
    {
        Guard.Argument(Value).NotEmpty(_ => "Name must be not empty")
            .NotNull("Name must has value");
    }
}