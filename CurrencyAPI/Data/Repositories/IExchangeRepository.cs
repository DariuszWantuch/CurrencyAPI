using CurrencyAPI.Models;
using CurrencyAPI.Models.DTO;

namespace CurrencyAPI.Data.Repositories
{
    public interface IExchangeRepository
    {
        void SaveRatesToDatabase(IEnumerable<ExchangeRateDTO> rates);

        List<ExchangeRate> GetRatesByPublication(DateTime publication);
    }
}
