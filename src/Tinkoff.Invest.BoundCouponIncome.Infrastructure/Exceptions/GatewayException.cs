namespace Tinkoff.Invest.BoundCouponIncome.Infrastructure.Exceptions;

public class GatewayException : Exception
{
    public GatewayException(string? message): base(message){}
}