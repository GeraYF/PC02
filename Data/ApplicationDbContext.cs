using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using examen.Models;

namespace examen.Data;


public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<examen.Models.Contacto> DataContacto {get; set; }
    public DbSet<examen.Models.Cuenta> DataCuenta {get; set; }


    public DbSet<Transaccion01> Transacciones { get; set; }

}
