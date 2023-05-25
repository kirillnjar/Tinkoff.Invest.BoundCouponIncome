using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Portfolios;

public record Portfolio(IReadOnlyCollection<Portfolio.Instrument> Instruments)
{
    public record Instrument(Figi Figi, Quantity Quantity, InstrumentType InstrumentType);
    
    public enum InstrumentType
    {
        Unknown = 0,
        Bond = 1
    }
}