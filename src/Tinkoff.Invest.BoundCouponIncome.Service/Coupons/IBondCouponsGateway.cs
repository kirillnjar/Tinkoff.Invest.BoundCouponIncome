using CSharpFunctionalExtensions;
using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Tinkoff.Invest.BoundCouponIncome.Core.Common;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Coupons;

public interface ICouponsGateway
{
    Task<Result<IReadOnlyCollection<CouponDto>>> Get(UserToken userToken, Figi bondFigi, CancellationToken cancellationToken);
}