using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Tinkoff.Invest.BoundCouponIncome.Core.Repayments;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Repayments;

public class RepaymentDto
{
    public RepaymentDto(Name accountName, Name instrumentName, DateTimeOffset date, Amount amount, RepaymentType type)
    {
        AccountName = accountName;
        InstrumentName = instrumentName;
        Date = date;
        Amount = amount;
        Type = type;
    }

    public Name AccountName { get; private set; }
    public Name InstrumentName { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public Amount Amount { get; private set; }
    public RepaymentType Type { get; private set; }
}