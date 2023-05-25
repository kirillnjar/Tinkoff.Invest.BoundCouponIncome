using CSharpFunctionalExtensions;
using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Bonds;

public interface IBondGateway
{
    Task<Result<BondDto>> Get(UserToken userToken, Figi figi, CancellationToken cancellationToken);
}