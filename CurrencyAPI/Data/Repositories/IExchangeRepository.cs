using CurrencyAPI.Models.DTO;

namespace CurrencyAPI.Data.Repositories
{
    public interface IExchangeRepository
    {
        public void SaveRatesToDatabase(IEnumerable<ExchangeRateDTO> rates)
        {

        }
    }
}
