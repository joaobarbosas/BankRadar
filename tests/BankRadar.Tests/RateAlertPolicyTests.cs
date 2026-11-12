using BankRadar.Models;
using BankRadar.Services;
using Xunit;

namespace BankRadar.Tests;

public class RateAlertPolicyTests
{
    [Fact]
    public void ReturnsOnlyRatesStrictlyBelowThreshold()
    {
        var rates = new[]
        {
            new Simulacao { Banco = "A", TaxaMensal = 1.5m },
            new Simulacao { Banco = "B", TaxaMensal = 2m },
            new Simulacao { Banco = "C", TaxaMensal = 2.5m }
        };

        var result = RateAlertPolicy.FindBelowThreshold(rates);

        Assert.Single(result);
        Assert.Equal("A", result[0].Banco);
    }

    [Fact]
    public void ReturnsEmptyListWhenNoRateQualifies()
    {
        var rates = new[] { new Simulacao { Banco = "A", TaxaMensal = 3m } };
        Assert.Empty(RateAlertPolicy.FindBelowThreshold(rates));
    }

    [Fact]
    public void SupportsCustomThreshold()
    {
        var rates = new[] { new Simulacao { Banco = "A", TaxaMensal = 2.5m } };
        Assert.Single(RateAlertPolicy.FindBelowThreshold(rates, 3m));
    }

    [Fact]
    public void RejectsNullCollection()
    {
        Assert.Throws<ArgumentNullException>(() => RateAlertPolicy.FindBelowThreshold(null!));
    }
}
