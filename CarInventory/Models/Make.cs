using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarInventory.Models
{
    public class Make
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
    }
}
