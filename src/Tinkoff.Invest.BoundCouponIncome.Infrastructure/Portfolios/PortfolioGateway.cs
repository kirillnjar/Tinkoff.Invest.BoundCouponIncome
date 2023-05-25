using CSharpFunctionalExtensions;
using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Tinkoff.Invest.BoundCouponIncome.Service.Portfolios;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace Tinkoff.Invest.BoundCouponIncome.Infrastructure.Portfolios;

public class PortfolioGateway : IPortfolioGateway
{
    public async Task<Result<Portfolio>> Get(UserToken userToken, Id accountId, CancellationToken cancellationToken)
    {
        try
        {
            var client = InvestApiClientFactory.Create(userToken.Value);
            var request = new PortfolioRequest()
            {
                AccountId = accountId.Value,
                Currency = PortfolioRequest.Types.CurrencyRequest.Rub
            };
            var response = await client.Operations.GetPortfolioAsync(request, cancellationToken: cancellationToken);

            return new Portfolio(response.Positions
                .Where(p => string.Equals(p.InstrumentType, "bond", StringComparison.OrdinalIgnoreCase))
                .Select(p => new Portfolio.Instrument(Figi.From(p.Figi), Quantity.From(ToIntQuantity(p.Quantity)),
                    Portfolio.InstrumentType.Bond)).ToList());
        }
        catch (Exception ex)
        {
            return Result.Failure<Portfolio>(
                $"Error when try to get portfolio from tinkoff api. Description: {ex.Message}");
        }
    }

    private int ToIntQuantity(Quotation quotation)
    {
        return (int) quotation.Units;
    }
}