using CsvHelper.Configuration.Attributes;

public class CarDataDTO
{
    [Name("carName")]
    public string CarName { get; set; } = null!;

    [Name("year")]
    public int Year { get; set; }

    [Name("engine")]
    public string Engine { get; set; } = null!;

    [Name("transmission")]
    public string Transmission { get; set; } = null!;

    [Name("bodyStyle")]
    public string BodyStyle { get; set; } = null!;

    [Name("price")]
    public decimal Price { get; set; }

    [Name("makeName")]
    public string MakeName { get; set; } = null!;

    [Name("country")]
    public string Country { get; set; } = null!;

    [Name("foundedYear")]
    public int FoundedYear { get; set; }
}
