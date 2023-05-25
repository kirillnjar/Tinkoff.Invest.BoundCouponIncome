namespace Tinkoff.Invest.BoundCouponIncome.Core.Common;

public class DateInterval
{
    public DateInterval(DateTimeOffset from, 
        DateTimeOffset to)
    {
        if (from >= to)
            throw new ArgumentException("From date must be bigger than to date in date interval");
        
        From = from;
        To = to;
    }

    public DateTimeOffset From { get; private set; }
    public DateTimeOffset To { get; private set; }
}