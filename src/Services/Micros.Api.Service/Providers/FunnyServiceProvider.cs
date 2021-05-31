using System;
using System.Threading.Tasks;
using Flurl.Http;
using Micros.Api.Service.Providers.Models;
using Microsoft.Extensions.Logging;

namespace Micros.Api.Service.Providers
{
    public abstract class FunnyServiceProvider : IFunnyServiceProvider
    {
        private readonly ILogger<FunnyServiceProvider> _logger;

        protected abstract string Host { get; }

        protected FunnyServiceProvider(ILogger<FunnyServiceProvider> logger)
        {
            _logger = logger;
        }

        public async Task<string> GetFactAsync()
        {
            var builder = new UriBuilder(Host) {Path = "/api/fact"};
            var result = await builder.Uri
                .ConfigureRequest(c => c.Timeout = new TimeSpan(0,0,10))
                .GetJsonAsync<FactDto>();
            return result.Fact;
        }

        public async Task<string> GetImageUrlAsync()
        {
            var builder = new UriBuilder(Host) { Path = "/api/image" };
            var result = await builder.Uri
                .ConfigureRequest(c => c.Timeout = new TimeSpan(0, 0, 10))
                .GetJsonAsync<Image>();
            return result.ImageUrl;
        }
    }
}