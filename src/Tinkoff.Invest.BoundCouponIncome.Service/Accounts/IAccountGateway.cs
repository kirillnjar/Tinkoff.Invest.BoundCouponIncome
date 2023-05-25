using CSharpFunctionalExtensions;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Accounts;

public interface IAccountsGateway
{
    Task<Result<IReadOnlyCollection<AccountDto>>> Get(UserToken userToken, CancellationToken token);
}