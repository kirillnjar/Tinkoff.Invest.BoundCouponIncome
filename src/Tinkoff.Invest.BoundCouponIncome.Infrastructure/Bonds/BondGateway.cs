using CSharpFunctionalExtensions;
using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Tinkoff.Invest.BoundCouponIncome.Core.Repayments;
using Tinkoff.Invest.BoundCouponIncome.Service.Bonds;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace Tinkoff.Invest.BoundCouponIncome.Infrastructure.Bonds;

public class BondGateway : IBondGateway
{
    public async Task<Result<BondDto>> Get(UserToken userToken, Figi figi, CancellationToken cancellationToken)
    {
        try
        {
            var client = InvestApiClientFactory.Create(userToken.Value);
            var request = new InstrumentRequest
            {
                IdType = InstrumentIdType.Figi,
                Id = figi.Value
            };
            var bonds = await client.Instruments.BondByAsync(request, cancellationToken: cancellationToken);

            return new BondDto(Figi.From(bonds.Instrument.Figi),
                Name.From(bonds.Instrument.Name),
                bonds.Instrument.MaturityDate.ToDateTimeOffset(),
                Amount.From(bonds.Instrument.Nominal));
        }
        catch (Exception ex)
        {
            return Result.Failure<BondDto>(
                $"Error when try to get bond info from tinkoff api. Description: {ex.Message}");
        }
    }
}