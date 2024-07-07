using CurrencyAPI.Models;
using CurrencyAPI.Models.DTO;

namespace CurrencyAPI.Services
{
    public interface IExchangeService
    {
        public Task<IEnumerable<RateDTO>> GetCurrentRatesFromNBP();
        public List<RateDTO> MapRatesFromDBToDTO(List<ExchangeRate> rates);
    }
}
