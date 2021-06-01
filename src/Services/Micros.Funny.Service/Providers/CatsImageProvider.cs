using System;
using System.Threading.Tasks;
using Flurl.Http;
using Micros.Funny.Service.Providers.Models;
using Microsoft.Extensions.Configuration;

namespace Micros.Funny.Service.Providers
{
    public class CatsImageProvider : IImageProvider
    {
        private readonly string _url;

        public CatsImageProvider(IConfiguration configuration)
        {
            _url = configuration["CatsImagesUrl"];
        }

        public async Task<string> GetImageUrlAsync()
        {
            var response = await _url.GetJsonAsync<CatImageResponse>();
            return response.Url;
        }
    }
}