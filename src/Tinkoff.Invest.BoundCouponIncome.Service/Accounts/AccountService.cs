using CSharpFunctionalExtensions;
using LinqSpecs;
using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Service.Accounts.InfoRetrive;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;

namespace Tinkoff.Invest.BoundCouponIncome.Service.Accounts;

public class AccountService : IAccountService
{
    private readonly IAccountsGateway _accountsGateway;
    private readonly IAccountInfoRetrieveService _accountsInfoRetrieveService;


    public AccountService(IAccountsGateway accountsGateway, IAccountInfoRetrieveService accountsInfoRetrieveService)
    {
        _accountsGateway = accountsGateway;
        _accountsInfoRetrieveService = accountsInfoRetrieveService;
    }

    public async Task<Result<IReadOnlyCollection<Account>>> Get(UserToken userToken, Specification<AccountDto> filter,
        CancellationToken token)
    {
        var allAccountsResult = await _accountsGateway.Get(userToken, token);

        if (allAccountsResult.IsFailure)
            return allAccountsResult.ConvertFailure<IReadOnlyCollection<Account>>();
        
        var retrievingTasks = allAccountsResult.Value
            .Where(a => a.Type is AccountType.Broker or AccountType.Iis && a.Status is AccountStatus.Open)
            .Where(filter.ToExpression().Compile().Invoke)
            .Select(a => Get(userToken, a, token));
        
        var accountInfoResults = await Task.WhenAll(retrievingTasks);

        var combinedAccountInfoResult = accountInfoResults.Combine(", ");
        return combinedAccountInfoResult.Map(r => (IReadOnlyCollection<Account>)r.ToList());
    }

    private async Task<Result<Account>> Get(UserToken userToken,
        AccountDto accountDto, CancellationToken token)
    {
        var infoResult = await _accountsInfoRetrieveService.Retrieve(userToken, accountDto.Id, token);

        if (infoResult.IsFailure)
            return infoResult.ConvertFailure<Account>();

        return Result.Success(new Account(accountDto.Id, accountDto.AccountName, infoResult.Value.Bonds));
    }
}