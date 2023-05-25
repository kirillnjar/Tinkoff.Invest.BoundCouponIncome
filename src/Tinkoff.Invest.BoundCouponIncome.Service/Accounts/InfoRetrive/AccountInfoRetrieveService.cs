using CSharpFunctionalExtensions;
using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Tinkoff.Invest.BoundCouponIncome.Core.Common;
using Tinkoff.Invest.BoundCouponIncome.Core.Coupons;
using Tinkoff.Invest.BoundCouponIncome.Core.Repayments;
using Tinkoff.Invest.BoundCouponIncome.Service.Bonds;
using Tinkoff.Invest.BoundCouponIncome.Service.Coupons;
using Tinkoff.Invest.BoundCouponIncome.Service.Portfolios;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Accounts.InfoRetrive;

public class AccountInfoRetrieveService : IAccountInfoRetrieveService
{
    private readonly IPortfolioGateway _portfolioGateway;
    private readonly IBondGateway _bondGateway;
    private readonly ICouponsGateway _couponsGateway;

    public AccountInfoRetrieveService(IPortfolioGateway portfolioGateway,
        IBondGateway bondGateway,
        ICouponsGateway couponsGateway)
    {
        _portfolioGateway = portfolioGateway;
        _bondGateway = bondGateway;
        _couponsGateway = couponsGateway;
    }

    public async Task<Result<AccountInfo>> Retrieve(UserToken userToken,
        Id accountId,
        CancellationToken token)
    {
        var portfolioResult = await _portfolioGateway.Get(userToken, accountId, token);

        if (portfolioResult.IsFailure)
            return portfolioResult.ConvertFailure<AccountInfo>();

        var bonds = new List<BondCollection>();
        foreach (var instrument in portfolioResult.Value.Instruments)
        {
            // only bound for now
            if (instrument.InstrumentType != Portfolio.InstrumentType.Bond)
                continue;

            var bondResult = await _bondGateway.Get(userToken, instrument.Figi, token);
            if (bondResult.IsFailure)
                return bondResult.ConvertFailure<AccountInfo>();

            var couponResult = await _couponsGateway.Get(userToken, instrument.Figi, token);
            if (couponResult.IsFailure)
                return couponResult.ConvertFailure<AccountInfo>();
            
            var coupons = couponResult.Value.Select(x => new Coupon(new Repayment(x.CouponDate, x.Amount))).ToList();
            var bond = new Bond(bondResult.Value.Figi, bondResult.Value.Name, coupons, bondResult.Value.MaturityDate, bondResult.Value.Nominal);
            bonds.Add(new BondCollection(bond, instrument.Quantity));
        }

        return Result.Success(new AccountInfo(accountId, bonds));
    }
}