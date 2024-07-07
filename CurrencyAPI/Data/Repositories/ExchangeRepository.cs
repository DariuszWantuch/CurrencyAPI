using CurrencyAPI.Models.DTO;
using CurrencyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyAPI.Data.Repositories
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ExchangeRepository(ApplicationDbContext context, ILogger<ExchangeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void SaveRatesToDatabase(IEnumerable<ExchangeRateDTO> rates)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var publicationDate = rates?.FirstOrDefault()?.PublicationDate;

                if (rates != null && 
                    rates.Any() && 
                    !_context.ExchangeRates.Any(x => x.Publication.PublicationDate.Equals(publicationDate)))
                {
                    foreach (var rate in rates)
                    {
                        var publication = new Publication
                        {
                            PublicationDate = rate.PublicationDate
                        };

                        _context.Publications.Add(publication);
                        _context.SaveChanges();

                        foreach (var currency in rate.CurrencyDTOs)
                        {
                            if (!_context.Currencies.Any(x => x.CurrencyCode.Equals(currency.CurrencyCode)))
                            {
                                var currencyEntity = new Currency
                                {
                                    CurrencyCode = currency.CurrencyCode,
                                    CurrencyName = currency.CurrencyName
                                };

                                _context.Currencies.Add(currencyEntity);
                                _context.SaveChanges();
                            }
                        }

                        foreach (var currency in rate.CurrencyDTOs)
                        {
                            var currencyEntity = _context.Currencies.FirstOrDefault(x => x.CurrencyCode.Equals(currency.CurrencyCode));

                            if(currencyEntity != null)
                            {
                                var exchangeRate = new ExchangeRate
                                {
                                    CurrencyId = currencyEntity.Id,
                                    PublicationId = publication.Id,
                                    Rate = currency.Rate
                                };

                                _context.ExchangeRates.Add(exchangeRate);
                                _context.SaveChanges();
                            }                         
                        }
                    }

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while saving exchange rates to the database: " + ex.Message, DateTime.UtcNow.ToLongTimeString());
                transaction.Rollback();
            }
        }

        public List<ExchangeRate> GetRatesByPublication(DateTime publication)
        {
            var rates = _context.ExchangeRates
                .Include(x => x.Currency)
                .Include(x => x.Publication)
                .Where(x => x.Publication.PublicationDate.Equals(publication))
                .ToList();

            return rates;
        }
    }
}
