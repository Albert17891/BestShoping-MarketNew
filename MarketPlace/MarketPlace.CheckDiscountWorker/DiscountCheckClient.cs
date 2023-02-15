using MarketPlace.CheckDiscountWorker.Models;
using Microsoft.Extensions.Options;

namespace MarketPlace.CheckDiscountWorker;

public class DiscountCheckClient
{
    private readonly ILogger _logger;
    private readonly UrlAddress _options;

    public DiscountCheckClient(ILogger<DiscountCheckClient> logger,IOptions<UrlAddress> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    public async Task CheckDiscount()
    {
        var httpClientHandler = new HttpClientHandler();
        httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
        {
            return true;
        };

        using (var client = new HttpClient(httpClientHandler))
        {
            try
            {
                client.BaseAddress = new Uri(_options.ServerAddress);

                var response = await client.GetAsync("Discount/check-discounts");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    _logger.LogInformation(result);
                }

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
            }


        }
    }
}
