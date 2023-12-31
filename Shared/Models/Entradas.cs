using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace _2doParcial_AP1RonellIntento.Shared;
public class Entradas
{
    [Key]
    public int EntradaId {get; set;}
    
    [Required(ErrorMessage ="El campo {0} es obligatorio")]
    public DateTime Fecha {get; set;} = DateTime.Now;

    [Required(ErrorMessage ="El campo {0} es obligatorio")]
    [StringLength(75, MinimumLength = 5, ErrorMessage = "El campo {0} debe de tener entre {2} y {1} caracteres")]
    public string Concepto {get; set;} = string.Empty;

    [Required(ErrorMessage ="El campo {0} es obligatorio")]
    public int PesoTotal {get; set;}

    [ForeignKey("ProductoId")]
    [Required(ErrorMessage ="El campo Producto es obligatorio")]
    public int ProductoId {get; set;}

    [Required(ErrorMessage ="El campo {0} es obligatorio")]
    public int CantidadProducida {get; set;}

    [ForeignKey("EntradaId")]
    public ICollection<EntradasDetalle> EntradasDetalle {get; set;} = new List<EntradasDetalle>();
}