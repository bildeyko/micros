using System;
using System.Threading.Tasks;
using Flurl.Http;
using Micros.Funny.Service.Providers.Models;
using Microsoft.Extensions.Configuration;

namespace Micros.Funny.Service.Providers
{
    public class DogsImageProvider : IImageProvider
    {
        private readonly string _url;

        public DogsImageProvider(IConfiguration configuration)
        {
            _url = configuration["DogsImagesUrl"];
        }

        public async Task<string> GetImageUrlAsync()
        {
            var response = await _url.GetJsonAsync<DogImageResponse>();
            return response.Message;
        }
    }
}