using Tinkoff.Invest.BoundCouponIncome.Core.Coupons;
using Tinkoff.Invest.BoundCouponIncome.Core.Repayments;

namespace Tinkoff.Invest.BoundCouponIncome.Core.Bonds;

public class Bond
{
    public Bond(Figi figi,
        Name name,
        IReadOnlyCollection<Coupon> coupons,
        DateTimeOffset maturityDate,
        Amount faceValue)
    {
        Figi = figi;
        Name = name;
        Coupons = coupons;
        MaturityDate = maturityDate;
        Nominal = faceValue;
    }

    public Figi Figi { get; private set; }
    public Name Name { get; private set; }
    public IReadOnlyCollection<Coupon> Coupons { get; private set; }
    public DateTimeOffset MaturityDate { get; private set; }
    public Amount Nominal { get; private set; }

    public IEnumerable<Repayment> GetCouponRepayments => Coupons.Select(x => x.Repayment);
    public Repayment GetMaturityRepayment => new Repayment(MaturityDate, Nominal);
}