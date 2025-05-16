namespace CarInventory.Dtos
{
    public class MakeStatsDTO
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int FoundedYear { get; set; }
        public List<string> Models { get; set; }
    }
}
