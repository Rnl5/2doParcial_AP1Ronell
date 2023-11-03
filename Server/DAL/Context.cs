using Microsoft.EntityFrameworkCore;
using _2doParcial_AP1RonellIntento.Shared;

namespace _2doParcial_AP1RonellIntento.Server;

public class Context : DbContext
{
   public Context(DbContextOptions<Context> options) : base(options) { }
   
   public DbSet<Entradas> Entradas {get; set;}

   public DbSet<Productos> Productos {get; set;}
}