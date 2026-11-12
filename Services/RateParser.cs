using System.Globalization;

namespace BankRadar.Services;

/// <summary>Parses non-negative rates as displayed by the Brazilian source.</summary>
public static class RateParser
{
    public static bool TryParse(string? text, out decimal rate)
    {
        rate = 0m;
        if (string.IsNullOrWhiteSpace(text)) return false;
        var normalized = text.Trim();
        if (normalized.EndsWith('%')) normalized = normalized[..^1].TrimEnd();
        // Group separators are intentionally unsupported to avoid ambiguous values.
        if (normalized.Contains(',') && normalized.Contains('.')) return false;
        normalized = normalized.Replace(',', '.');
        return decimal.TryParse(normalized, NumberStyles.AllowDecimalPoint,
            CultureInfo.InvariantCulture, out rate);
    }
}
