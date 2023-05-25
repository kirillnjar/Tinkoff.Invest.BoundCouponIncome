using Dawn;
using ValueOf;

namespace Tinkoff.Invest.BoundCouponIncome.Core.Accounts;

public class Id : ValueOf<string, Id>
{
    protected override void Validate()
    {
        Guard.Argument(Value).NotEmpty(_ => "Id must be not empty")
            .NotNull("Id must has value");
    }
}