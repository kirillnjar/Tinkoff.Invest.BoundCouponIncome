using System.Globalization;
using System.Text;
using Tinkoff.Invest.BoundCouponIncome.Service.Repayments;

namespace Tinkoff.Invest.BoundCouponIncome.Infrastructure.Repayments;

class RepaymentsCsvConverter : IRepaymentsCsvConverter
{
    public byte[] GetInBytes(IReadOnlyCollection<RepaymentDto> repaymentsCollection)
    {
        var csvBuilder = new StringBuilder();
        var columnNames = new[]
        {
            nameof(RepaymentDto.AccountName), nameof(RepaymentDto.InstrumentName),
            nameof(RepaymentDto.Date), nameof(RepaymentDto.Type), nameof(RepaymentDto.Amount)
        };
        csvBuilder.AppendLine(string.Join(';', columnNames));
        foreach (var repayment in repaymentsCollection)
        {
            var row = new[]
            {
                repayment.AccountName.Value, repayment.InstrumentName.Value,
                repayment.Date.ToString(), repayment.Type.ToString(), 
                repayment.Amount.Value.ToString(CultureInfo.InvariantCulture)
            };
            csvBuilder.AppendLine(string.Join(';', row));
        }

        return Encoding.UTF8.GetBytes(csvBuilder.ToString());
    }
}