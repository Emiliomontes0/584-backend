using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarInventory.Models
{
    public class CarModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int MakeId { get; set; }

        public Make Make { get; set; }
    }
}
