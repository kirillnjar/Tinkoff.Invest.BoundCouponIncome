using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Tinkoff.Invest.BoundCouponIncome.Core.Repayments;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Bonds;

public record BondDto(Figi Figi, Name Name, DateTimeOffset MaturityDate, Amount Nominal);