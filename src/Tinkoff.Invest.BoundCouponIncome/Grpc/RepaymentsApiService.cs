using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Tinkoff.Invest.BoundCouponIncome.Api;
using Tinkoff.Invest.BoundCouponIncome.Service.Repayments;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;

namespace Tinkoff.Invest.BoundCouponIncome.Grpc;

public class RepaymentsApiService : Api.RepaymentsApi.RepaymentsApiBase
{
    private readonly IRepaymentsService _repaymentsService;

    public RepaymentsApiService(IRepaymentsService repaymentsService)
    {
        _repaymentsService = repaymentsService;
    }

    public override async Task<GetAllRepaymentsResponse> GetAllRepayments(GetAllRepaymentsRequest request, ServerCallContext context)
    {
        var repayments = await _repaymentsService.Get(UserToken.From(request.ApiKey), context.CancellationToken);
        return new GetAllRepaymentsResponse()
        {
            Repayments =
            {
                repayments.Select(r => new Repayments
                {
                    AccountName = r.AccountName.Value,
                    InstrumentName = r.InstrumentName.Value,
                    Date = Timestamp.FromDateTimeOffset(r.Date),
                    Amount = Decimal.ToDouble(r.Amount.Value),
                    Type = ToRepaymentType(r.Type)
                })
            }
        };
    }

    private static Repayments.Types.RepaymentsType ToRepaymentType(RepaymentType repaymentType)
    {
        return repaymentType switch
        {
            RepaymentType.Coupon => Repayments.Types.RepaymentsType.Coupon,
            RepaymentType.Maturity => Repayments.Types.RepaymentsType.Maturity,
            _ => Repayments.Types.RepaymentsType.Unknwon
        };
    }
}