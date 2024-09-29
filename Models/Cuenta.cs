using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace examen.Models
{
    [Table("t_cuenta")]
    public class Cuenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {   get; set;}
        [NotNull]
        public string? Name { get; set; }
        [NotNull]
        public string? Tipo { get; set; }
        [NotNull]
        public Decimal Saldo { get; set; }
        [NotNull]
        public string? Email { get; set; }
    }
}


