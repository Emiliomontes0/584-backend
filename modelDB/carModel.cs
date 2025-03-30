using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace modelDB;

[Table("car_models")]
public partial class CarModel
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("year")]
    public int Year { get; set; }

    [Column("engine")]
    [StringLength(50)]
    [Unicode(false)]
    public string Engine { get; set; } = null!;

    [Column("transmission")]
    [StringLength(50)]
    [Unicode(false)]
    public string Transmission { get; set; } = null!;

    [Column("body_style")]
    [StringLength(50)]
    [Unicode(false)]
    public string BodyStyle { get; set; } = null!;

    [Column("price")]
    public decimal Price { get; set; }

    [Column("make_id")]
    public int MakeId { get; set; }

    [ForeignKey("MakeId")]
    [InverseProperty("CarModels")]
    public virtual Make Make { get; set; } = null!;
}
