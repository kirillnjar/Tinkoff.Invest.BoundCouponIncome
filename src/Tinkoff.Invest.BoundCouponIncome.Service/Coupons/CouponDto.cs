using Tinkoff.Invest.BoundCouponIncome.Core.Repayments;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Coupons;

public record CouponDto(DateTimeOffset CouponDate, Amount Amount);