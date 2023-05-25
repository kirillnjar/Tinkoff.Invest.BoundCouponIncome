using Dawn;
using ValueOf;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Token;

public class UserToken : ValueOf<string, UserToken>
{
    protected override void Validate()
    {
        Guard.Argument(Value).NotEmpty(_ => "Name must be not empty")
            .NotNull("Name must has value");
    }
}