using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;

namespace Tinkoff.Invest.BoundCouponIncome.Core.Accounts;

public class Account
{
    public Account(Id id, Name name, IReadOnlyCollection<BondCollection> bondCollection)
    {
        Id = id;
        Name = name;
        BondCollection = bondCollection;
    }

    public Id Id { get; private set; }
    public Name Name { get; private set; }
    public IReadOnlyCollection<BondCollection> BondCollection { get; private set; }
}