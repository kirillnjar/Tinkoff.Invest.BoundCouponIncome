using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Tinkoff.Invest.BoundCouponIncome.Api;
using Tinkoff.Invest.BoundCouponIncome.Core.Common;
using Tinkoff.Invest.BoundCouponIncome.Infrastructure.Repayments;
using Tinkoff.Invest.BoundCouponIncome.Service.Repayments;
using Tinkoff.Invest.BoundCouponIncome.Service.Token;

namespace Tinkoff.Invest.BoundCouponIncome.Controllers.V1;

[ApiController]
[Route("/v1/repayments")]
[Produces("application/json")]
public class RepaymentsApiService : ControllerBase
{
    private readonly IRepaymentsService _repaymentsService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IRepaymentsCsvConverter _repaymentsCsvConverter;

    public RepaymentsApiService(IRepaymentsService repaymentsService, 
        IDateTimeProvider dateTimeProvider, 
        IRepaymentsCsvConverter repaymentsCsvConverter)
    {
        _repaymentsService = repaymentsService;
        _dateTimeProvider = dateTimeProvider;
        _repaymentsCsvConverter = repaymentsCsvConverter;
    }
    
    [HttpGet("/v1/repayments/as-csv")]
    public async Task<FileResult> GetAllRepaymentsAsCsv([FromQuery]string apiKey, CancellationToken token)
    {
        var repayments = await _repaymentsService.Get(UserToken.From(apiKey), token);
        var csv = _repaymentsCsvConverter.GetInBytes(repayments);
        var now = _dateTimeProvider.UtcNow();
        string fileName = $"repayments-by-{now}.csv";

        return File(csv, "text/csv", fileName);
    }

    [HttpGet("/v1/repayments")]
    public async Task<GetAllRepaymentsResponse> GetAllRepayments([FromQuery]string apiKey, CancellationToken token)
    {
        var repayments = await _repaymentsService.Get(UserToken.From(apiKey), token);
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