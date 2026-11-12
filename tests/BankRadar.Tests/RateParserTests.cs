using BankRadar.Services;
using Xunit;

namespace BankRadar.Tests;

public class RateParserTests
{
    [Theory]
    [InlineData("1,25", "1.25")]
    [InlineData(" 1.25 % ", "1.25")]
    [InlineData("0", "0")]
    [InlineData("12,345", "12.345")]
    public void ParsesSupportedSourceFormats(string input, string expected)
    {
        Assert.True(RateParser.TryParse(input, out var value));
        Assert.Equal(decimal.Parse(expected, System.Globalization.CultureInfo.InvariantCulture), value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("N/D")]
    [InlineData("-1,2")]
    [InlineData("1.234,56")]
    [InlineData("1,2,3")]
    [InlineData("99999999999999999999999999999999999999")]
    public void RejectsMissingMalformedNegativeAndOverflowValues(string? input)
    {
        Assert.False(RateParser.TryParse(input, out _));
    }
}
