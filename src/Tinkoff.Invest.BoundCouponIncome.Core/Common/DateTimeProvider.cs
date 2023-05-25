namespace Tinkoff.Invest.BoundCouponIncome.Core.Common;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow()
    {
        return DateTimeOffset.UtcNow;
    }
}