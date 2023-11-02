using System.ComponentModel.DataAnnotations;

public class EntradasDetalle
{
    [Key]
    public int DetalleId {get; set;}

    public int EntradaId {get; set;}

    public int ProductoId {get; set;}

    public float CantidadUtilizada {get; set;}
}