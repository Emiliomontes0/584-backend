using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarInventory.Models
{
    public class CarModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public int Year { get; set; }

        public string Engine { get; set; } = null!;

        public string Transmission { get; set; } = null!;

        public string BodyStyle { get; set; } = null!;

        public decimal Price { get; set; }

        [ForeignKey("Make")]
        public int MakeId { get; set; }
        public Make Make { get; set; } = null!;
    }
}
