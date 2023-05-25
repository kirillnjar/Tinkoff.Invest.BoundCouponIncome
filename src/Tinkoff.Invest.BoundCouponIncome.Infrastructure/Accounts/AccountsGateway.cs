using CSharpFunctionalExtensions;
using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Core.Bonds;
using Tinkoff.Invest.BoundCouponIncome.Service.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;
using ContractAccountStatus = Tinkoff.InvestApi.V1.AccountStatus;
using DomainAccountStatus = Tinkoff.Invest.BoundCouponIncome.Service.Accounts.AccountStatus;
using ContractAccountType = Tinkoff.InvestApi.V1.AccountType;
using DomainAccountType = Tinkoff.Invest.BoundCouponIncome.Service.Accounts.AccountType;

namespace Tinkoff.Invest.BoundCouponIncome.Infrastructure.Accounts;

public class AccountsGateway : IAccountsGateway
{
    public async Task<Result<IReadOnlyCollection<AccountDto>>> Get(UserToken userToken, CancellationToken token)
    {
        try
        {
            var client = InvestApiClientFactory.Create(userToken.Value);
            var accounts = await client.Users.GetAccountsAsync(new GetAccountsRequest(), cancellationToken: token);

            return accounts.Accounts.Select(a => new AccountDto(Id.From(a.Id), ToDomainAccountType(a.Type),
                Name.From(a.Name), ToDomainAccountState(a.Status))).ToList();
        }
        catch (Exception ex)
        {
            return Result.Failure<IReadOnlyCollection<AccountDto>>(
                $"Error when try to get accounts from tinkoff api. Description: {ex.Message}");
        }
    }

    private DomainAccountType ToDomainAccountType(ContractAccountType type)
    {
        return type switch
        {
            ContractAccountType.Tinkoff => DomainAccountType.Broker,
            ContractAccountType.TinkoffIis => DomainAccountType.Iis,
            ContractAccountType.InvestBox => DomainAccountType.InvestBox,
            _ => DomainAccountType.Unknown
        };
    }

    private DomainAccountStatus ToDomainAccountState(ContractAccountStatus status)
    {
        return status switch
        {
            ContractAccountStatus.Closed => DomainAccountStatus.Closed,
            ContractAccountStatus.New => DomainAccountStatus.New,
            ContractAccountStatus.Open => DomainAccountStatus.Open,
            _ => DomainAccountStatus.Unknown
        };
    }
}