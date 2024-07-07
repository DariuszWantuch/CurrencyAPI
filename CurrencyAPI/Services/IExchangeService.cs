using CurrencyAPI.Models.DTO;

namespace CurrencyAPI.Services
{
    public interface IExchangeService
    {
        Task<IEnumerable<ExchangeRateDTO>> GetCurrentRatesFromNBP();
    }
}
