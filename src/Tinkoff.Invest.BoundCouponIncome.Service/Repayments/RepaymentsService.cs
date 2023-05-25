using CSharpFunctionalExtensions;
using LinqSpecs;
using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Tinkoff.Invest.BoundCouponIncome.Core.Common;
using Tinkoff.Invest.BoundCouponIncome.Core.Exceptions;
using Tinkoff.Invest.BoundCouponIncome.Service.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Repayments;

class RepaymentsService : IRepaymentsService
{
    private readonly IAccountService _accountService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RepaymentsService(IAccountService accountService,
        IDateTimeProvider dateTimeProvider)
    {
        _accountService = accountService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<IReadOnlyCollection<RepaymentDto>> Get(UserToken userToken, Specification<AccountDto> filter,
        CancellationToken token)
    {
        var accounts = await _accountService.Get(userToken, filter, token)
            .Match(r => r, e => throw new DomainException(e));
        var now = _dateTimeProvider.UtcNow();
        return accounts.SelectMany(a => a.BondCollection.SelectMany(bc => ToRepaymentDto(a.Name, bc)))
            .Where(r => r.Date > now).OrderBy(d => d.Date).ToList();
    }

    private IEnumerable<RepaymentDto> ToRepaymentDto(Name accountName, BondCollection collection)
    {
        var maturityRepayment = collection.CombineMaturityRepaymentsByQuantity();
        return collection.CombineCouponRepaymentsByQuantity()
            .Select(r => new RepaymentDto(accountName, collection.Bond.Name, r.Date, r.Amount, RepaymentType.Coupon))
            .Append(new RepaymentDto(accountName, collection.Bond.Name, maturityRepayment.Date,
                maturityRepayment.Amount,
                RepaymentType.Maturity));
    }
}