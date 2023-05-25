using CSharpFunctionalExtensions;
using LinqSpecs;
using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Accounts;

public interface IAccountService
{
    Task<Result<IReadOnlyCollection<Account>>> Get(UserToken userToken,
        Specification<AccountDto> filter,
        CancellationToken token);
}