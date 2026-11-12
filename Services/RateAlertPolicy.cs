using BankRadar.Models;

namespace BankRadar.Services;

public static class RateAlertPolicy
{
    public const decimal DefaultMonthlyThreshold = 2m;

    public static List<Simulacao> FindBelowThreshold(
        IEnumerable<Simulacao> rates,
        decimal threshold = DefaultMonthlyThreshold)
    {
        ArgumentNullException.ThrowIfNull(rates);
        return rates.Where(rate => rate.TaxaMensal < threshold).ToList();
    }
}
