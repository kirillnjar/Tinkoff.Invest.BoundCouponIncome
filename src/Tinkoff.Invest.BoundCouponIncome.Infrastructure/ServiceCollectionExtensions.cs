using Microsoft.Extensions.DependencyInjection;
using Tinkoff.Invest.BoundCouponIncome.Core.Accounts;
using Tinkoff.Invest.BoundCouponIncome.Core.Common;
using Tinkoff.Invest.BoundCouponIncome.Infrastructure.Coupons;
using Tinkoff.Invest.BoundCouponIncome.Service.Accounts;

namespace Tinkoff.Invest.BoundCouponIncome.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.Scan(x => x.FromAssemblies(
                typeof(CouponsGateway).Assembly,
                typeof(AccountService).Assembly,
                typeof(Account).Assembly)
            .AddClasses(c => c.Where(t => t.Name.EndsWith("Service")
                                          || t.Name.EndsWith("Gateway")
                                          || t.Name.EndsWith("Converter")))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}