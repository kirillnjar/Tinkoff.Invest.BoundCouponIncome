using CSharpFunctionalExtensions;
using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Portfolios;

public interface IPortfolioGateway
{
    Task<Result<Portfolio>> Get(UserToken userToken, Id accountId, CancellationToken token);
}