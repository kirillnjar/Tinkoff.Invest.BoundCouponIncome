using Tinkoff.Invest.BoundCouponIncome.Service.Repayments;

namespace Tinkoff.Invest.BoundCouponIncome.Infrastructure.Repayments;

public interface IRepaymentsCsvConverter
{
    byte[] GetInBytes(IReadOnlyCollection<RepaymentDto> repaymentsCollection);
}