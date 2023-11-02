using System.ComponentModel.DataAnnotations;

public class Productos
{
    [Key]
    public int ProductoId {get; set;}

    public string Descripcion {get; set;} = string.Empty;

    public short Tipo {get; set;}

    public int Existencia {get; set;}
}