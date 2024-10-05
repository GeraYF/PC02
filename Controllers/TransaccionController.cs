using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using examen.Data; // Asegúrate de tener este espacio de nombres
using examen.Models; // Asegúrate de tener este espacio de nombres

namespace PC02.Controllers
{
    
    public class TransaccionController : Controller
    {
        private readonly ILogger<TransaccionController> _logger;
        private readonly ApplicationDbContext _context; // Inyección del contexto de la base de datos

        // Constructor que inyecta el logger y el ApplicationDbContext
        public TransaccionController(ILogger<TransaccionController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; // Asignación del contexto de la base de datos
        }

        // GET: Mostrar formulario para registrar una transacción
        [HttpGet("Registrar")]
        public IActionResult Registrar()
        {
            return View();
        }

        // POST: Procesar los datos del formulario y registrar la transacción
        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar(Transaccion01 transaccion)
        {
            if (ModelState.IsValid)
            {
                if (transaccion.Moneda == "BTC")
                {
                    // Si la moneda seleccionada es BTC, convertimos el monto de USD a BTC
                    transaccion.MontoFinal = await ConvertUsdToBtc(transaccion.MontoEnviado);
                }
                else
                {
                    // Si la moneda es USD, simplemente se asigna el monto enviado
                    transaccion.MontoFinal = transaccion.MontoEnviado;
                }

                _context.Transacciones.Add(transaccion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Listado");
            }
            return View(transaccion);
        }

        // Método para convertir USD a BTC usando la API de CoinGecko
        private async Task<decimal> ConvertUsdToBtc(decimal usdAmount)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CoinGeckoResponse>(jsonResponse);

                    // Verifica que los datos no sean nulos
                    if (data?.Bitcoin?.Usd != null)
                    {
                        decimal btcRate = data.Bitcoin.Usd.Value; // Acceso seguro a la propiedad
                        return usdAmount / btcRate;
                    }
                }
                throw new Exception("Error al obtener la tasa de conversión.");
            }
        }

        // Método para listar las transacciones
        [HttpGet("Listado")]
        public IActionResult Listado()
        {
            _logger.LogInformation("Accediendo al listado de transacciones.");
            var transacciones = _context.Transacciones.ToList(); // Obtener las transacciones de la base de datos
            return View(transacciones); // Pasar los datos a la vista
        }

        // Método para la vista principal
        public IActionResult Index()
        {
            return View();
        }

        // Manejo de errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
