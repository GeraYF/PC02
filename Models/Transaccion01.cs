using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace examen.Models
{
    public class Transaccion01
    {
        public int Id { get; set; }
        public string? Remitente { get; set; }
        public string? Destinatario { get; set; }
        public string? PaisOrigen { get; set; }
        public string? PaisDestino { get; set; }
        public decimal MontoEnviado { get; set; }
        public decimal TasaCambio { get; set; }
        public decimal MontoFinal { get; set; }
        public string? Estado { get; set; }
        public string? Moneda { get; set; } // USD o BT
    }
}
