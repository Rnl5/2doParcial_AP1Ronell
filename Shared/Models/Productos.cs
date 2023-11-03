using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2doParcial_AP1RonellIntento.Shared;
public class Productos
{
    [Key]
    public int ProductoId {get; set;}

    [Required(ErrorMessage ="El campo {0} es obligatorio")]
    [StringLength(50, MinimumLength = 3, ErrorMessage ="Debe de insertar un dato entre {2} y {1} caracteres")]
    public string Descripcion {get; set;} = string.Empty;

    [Required(ErrorMessage ="El campo {0} es obligatorio")]
    [Range(0,15, ErrorMessage ="El dato insertado debe de estar entre {1} y {2}")]
    public short Tipo {get; set;}
    
    [Required(ErrorMessage ="El campo {0} es obligatorio")]
    [Range(0,1500, ErrorMessage ="El dato insertado debe de estar entre {1} y {2}")]
    public int Existencia {get; set;}

    [ForeignKey("Productoid")]
    public ICollection<EntradasDetalle> EntradasDetalles {get; set;} = new List<EntradasDetalle>();
}