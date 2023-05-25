using Tinkoff.Invest.BoundCouponIncome.Core.Repayments;

namespace Tinkoff.Invest.BoundCouponIncome.Core.Coupons;

public class Coupon
{
    public Coupon(Repayment repayment)
    {
        Repayment = repayment;
    }

    public Repayment Repayment { get; private set; }
}