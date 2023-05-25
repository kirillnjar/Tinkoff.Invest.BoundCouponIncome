using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Accounts;

public record AccountDto(Id Id, AccountType Type, Name AccountName, AccountStatus Status);