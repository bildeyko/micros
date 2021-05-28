using System.Threading.Tasks;
using Flurl.Http;
using Micros.Funny.Service.Providers.Models;
using Microsoft.Extensions.Configuration;

namespace Micros.Funny.Service.Providers
{
    public class CatsFactProvider : IFactProvider
    {
        private readonly string _url;

        public CatsFactProvider(IConfiguration configuration)
        {
            _url = configuration["CatsFactsUrl"];
        }

        public async Task<string> GetFactAsync()
        {
            var response = await _url.GetJsonAsync<CatFactResponse>();
            return response.Text;
        }
    }
}