using CurrencyAPI.Data;
using CurrencyAPI.Data.Repositories;
using CurrencyAPI.Models;
using CurrencyAPI.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CurrencyAPI.Services
{
    public class ExchangeService: IExchangeService
    {
        private readonly IHttpClientFactory _clientFactory;  
        private readonly IExchangeRepository _exchangeRepository;

        public ExchangeService(IHttpClientFactory clientFactory, IExchangeRepository exchangeRepository)
        {
            _clientFactory = clientFactory;      
            _exchangeRepository = exchangeRepository;
        }

        public async Task<IEnumerable<ExchangeRateDTO>> GetCurrentRatesFromNBP()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://api.nbp.pl/api/exchangerates/tables/A");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var rates = await System.Text.Json.JsonSerializer.DeserializeAsync
                       <IEnumerable<ExchangeRateDTO>>(responseStream);

                _exchangeRepository.SaveRatesToDatabase(rates);               

                return rates;
            }
            else
            {
                return null;
            }
        }      
    }
}
