using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

public class Entradas
{
    [Key]
    public int EntradaId {get; set;}

    public DateTime Fecha {get; set;} = DateTime.Now;

    public string Concepto {get; set;} = string.Empty;

    public int PesoTotal {get; set;}

    public int ProductoId {get; set;}

    public int CantidadProducida {get; set;}
}