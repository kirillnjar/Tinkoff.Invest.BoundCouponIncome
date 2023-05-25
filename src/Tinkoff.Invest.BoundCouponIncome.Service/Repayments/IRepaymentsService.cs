using LinqSpecs;
using Tinkoff.Invest.BoundCouponIncome.Core.Common;
using Tinkoff.Invest.BoundCouponIncome.Service.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Repayments;

public interface IRepaymentsService
{
    Task<IReadOnlyCollection<RepaymentDto>> Get(UserToken userToken,
        Specification<AccountDto> filter,
        CancellationToken token);

    Task<IReadOnlyCollection<RepaymentDto>> Get(UserToken userToken,
        CancellationToken token) => Get(userToken, new AdHocSpecification<AccountDto>(_ => true), token);
}