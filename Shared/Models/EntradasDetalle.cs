using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using _2doParcial_AP1RonellIntento.Shared;

namespace _2doParcial_AP1RonellIntento.Shared;
public class EntradasDetalle
{
    [Key]
    public int DetalleId {get; set;}

    [ForeignKey("EntradaId")]
    public int EntradaId {get; set;}

    [ForeignKey("ProductoId")]
    public int ProductoId {get; set;}
    
    [Required(ErrorMessage ="El campo {0} es obligatorio")]
    [Range(1,200, ErrorMessage ="EL dato insertado en el campo {0} debe de estar entre {1} y {2}")]
    public int CantidadUtilizada {get; set;}
}