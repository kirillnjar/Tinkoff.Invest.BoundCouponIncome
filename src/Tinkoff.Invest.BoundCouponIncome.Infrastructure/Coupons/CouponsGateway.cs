using CSharpFunctionalExtensions;
using Google.Protobuf.WellKnownTypes;
using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Tinkoff.Invest.BoundCouponIncome.Core.Common;
using Tinkoff.Invest.BoundCouponIncome.Core.Repayments;
using Tinkoff.Invest.BoundCouponIncome.Service.Coupons;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace Tinkoff.Invest.BoundCouponIncome.Infrastructure.Coupons;

public class CouponsGateway : ICouponsGateway
{
    public async Task<Result<IReadOnlyCollection<CouponDto>>> Get(UserToken userToken, Figi bondFigi,
        CancellationToken cancellationToken)
    {
        try
        {
            var client = InvestApiClientFactory.Create(userToken.Value);
            var request = new GetBondCouponsRequest()
            {
                Figi = bondFigi.Value,
                From = DateTimeOffset.MinValue.ToTimestamp(),
                To = DateTimeOffset.MaxValue.ToTimestamp()
            };
            var coupons = await client.Instruments.GetBondCouponsAsync(request, cancellationToken: cancellationToken);

            return coupons.Events
                .Select(e => new CouponDto(e.CouponDate.ToDateTimeOffset(), Amount.From(e.PayOneBond)))
                .ToList();
        }
        catch (Exception ex)
        {
            return Result.Failure<IReadOnlyCollection<CouponDto>>(
                $"Error when try to get bond info from tinkoff api. Description: {ex.Message}");
        }
    }
}