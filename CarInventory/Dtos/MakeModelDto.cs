namespace CarInventory.Dtos
{
    public class MakeModelDto
    {
        public int Id { get; set; }

        public string ModelName { get; set; } = null!;

        public int Year { get; set; }

        public string MakeName { get; set; } = null!;

        public string Engine { get; set; } = null!;

        public string Transmission { get; set; } = null!;

        public string BodyStyle { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
