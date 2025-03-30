using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace modelDB;

[Table("makes")]
public partial class Make
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("country")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Country { get; set; }

    [Column("founded_year")]
    public int? FoundedYear { get; set; }

    [InverseProperty("Make")]
    public virtual ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
}
