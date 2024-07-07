using CurrencyAPI.Data;
using CurrencyAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CurrencyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyRateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IExchangeService _exchangeService;

        private readonly ILogger _logger;

        public CurrencyRateController(ApplicationDbContext context, 
            IExchangeService exchangeService, 
            ILogger<CurrencyRateController> logger)
        {
            _context = context;
            _exchangeService = exchangeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentRates()
        {
            try
            {
                var rates = await _exchangeService.GetCurrentRatesFromNBP();

                if(rates == null)
                {
                    return NotFound();
                }
                
                return Ok(rates);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting currency rates from NBP: " + ex.Message, DateTime.UtcNow.ToLongTimeString());
                return NotFound();
            }                 
        }
    }
}
