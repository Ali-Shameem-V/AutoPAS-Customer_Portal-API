using System;
using System.Collections.Generic;

namespace angularAPI.Model;

public partial class Policy
{
    public string PolicyId { get; set; } = null!;

    public int? AppUserId { get; set; }

    public int? PolicyNumber { get; set; }

    public int QuoteNumber { get; set; }

    public DateOnly? PolicyEffectiveDt { get; set; }

    public DateOnly? PolicyExpirationDt { get; set; }

    public string? Status { get; set; }

    public int? Term { get; set; }

    public DateOnly? RateDt { get; set; }

    public decimal? TotalPremium { get; set; }

    public decimal? Sgst { get; set; }

    public decimal? Cgst { get; set; }

    public decimal? Igst { get; set; }

    public decimal? TotalFees { get; set; }

    public string? PaymentType { get; set; }

    public string? ReceiptNumber { get; set; }

    public sbyte? EligibleForNcb { get; set; }
}
