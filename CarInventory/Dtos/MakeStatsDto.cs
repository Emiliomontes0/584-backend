public class MakeStatsDTO
{
    public string MakeName { get; set; } = null!;
    public string Country { get; set; } = null!;
    public int FoundedYear { get; set; }
    public int NumberOfModels { get; set; }
    public decimal AveragePrice { get; set; }
}
