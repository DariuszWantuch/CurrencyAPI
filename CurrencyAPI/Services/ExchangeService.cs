using CurrencyAPI.Data;
using CurrencyAPI.Data.Repositories;
using CurrencyAPI.Models;
using CurrencyAPI.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CurrencyAPI.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IHttpClientFactory _clientFactory;  
        private readonly IExchangeRepository _exchangeRepository;

        public ExchangeService(IHttpClientFactory clientFactory, IExchangeRepository exchangeRepository)
        {
            _clientFactory = clientFactory;      
            _exchangeRepository = exchangeRepository;
        }

        public async Task<IEnumerable<RateDTO>> GetCurrentRatesFromNBP()
        {

            IEnumerable<RateDTO> rateDTOs = null;

            var existingRates = _exchangeRepository.GetRatesByPublication(DateTime.Now.Date);

            if (existingRates != null && existingRates.Any())
            {
                rateDTOs = MapRatesFromDBToDTO(existingRates);
                return rateDTOs;
            }

            var request = new HttpRequestMessage(HttpMethod.Get, "http://api.nbp.pl/api/exchangerates/tables/A");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var rates = await System.Text.Json.JsonSerializer.DeserializeAsync
                       <IEnumerable<ExchangeRateDTO>>(responseStream);

                if(rates != null)
                {
                    _exchangeRepository.SaveRatesToDatabase(rates);

                    var publication = rates.FirstOrDefault();

                    if (publication != null)
                    {
                        var ratesDB = _exchangeRepository.GetRatesByPublication(publication.PublicationDate);

                        if (ratesDB != null)
                        {
                            rateDTOs = MapRatesFromDBToDTO(ratesDB);
                        }
                    }                   
                }

                return rateDTOs;
            }
            else
            {
                return null;
            }
        }     
        
        public List<RateDTO> MapRatesFromDBToDTO(List<ExchangeRate> rates)
        {
            var ratesDTO = new List<RateDTO>();

            foreach (var rate in rates)
            {
                var rateDTO = new RateDTO
                {
                    Id = rate.Id,
                    CurrencyCode = rate.Currency.CurrencyCode,
                    CurrencyName = rate.Currency.CurrencyName,
                    Rate = rate.Rate,
                    PublicationDate = rate.Publication.PublicationDate.ToShortDateString()
                };

                ratesDTO.Add(rateDTO);
            }

            return ratesDTO;
        }
    }
}
