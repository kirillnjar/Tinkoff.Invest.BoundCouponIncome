namespace Tinkoff.Invest.BoundCouponIncome.Core.Common;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow();
}