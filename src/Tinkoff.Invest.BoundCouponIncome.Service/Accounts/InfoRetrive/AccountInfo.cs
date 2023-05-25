using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Accounts.InfoRetrive;

public record AccountInfo(Id AccountId, IReadOnlyCollection<BondCollection> Bonds);