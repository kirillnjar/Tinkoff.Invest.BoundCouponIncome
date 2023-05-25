using CSharpFunctionalExtensions;
using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Accounts.InfoRetrive;

public interface IAccountInfoRetrieveService
{
    Task<Result<AccountInfo>> Retrieve(UserToken userToken,
        Id accountId,
        CancellationToken token);
}