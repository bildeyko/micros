using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using Micros.Funny.Service.Providers.Models;
using Microsoft.Extensions.Configuration;

namespace Micros.Funny.Service.Providers
{
    public class DogsFactProvider : IFactProvider
    {
        private readonly string _url;

        public DogsFactProvider(IConfiguration configuration)
        {
            _url = configuration["DogsFactsUrl"];
        }

        public async Task<string> GetFactAsync()
        {
            var response = await _url.GetJsonAsync<List<DogFactResponse>>();
            return response?.FirstOrDefault()?.Fact;
        }
    }
}