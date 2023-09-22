namespace angularAPI.Model.Dto
{
    public class PolicyDetailsDto
    {
    public int? PolicyNumber { get; set; }
    public  DateOnly? PolicyEffectiveDt { get; set; }
    public DateOnly? PolicyExpirationDt { get; set; }
    public int? Term  { get; set; }
    public string? Status { get; set; }
    public decimal? TotalPremium { get; set; }
    }
}
