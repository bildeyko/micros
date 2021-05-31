using System;
using System.Threading.Tasks;
using Flurl.Http;
using Micros.Api.Service.Providers.Models;

namespace Micros.Api.Service.Providers
{
    public class ImageGeneratorServiceProvider : IImageGeneratorServiceProvider
    {
        private readonly string _host = Environment.GetEnvironmentVariable("IMAGE_GENERATOR_SERVICE_URL");

        public ImageGeneratorServiceProvider()
        {
            
        }

        public async Task<string> GenerateAsync(string title, string imageUrl)
        {
            var builder = new UriBuilder(_host) { Path = "/api" };
            var result = await builder.Uri.PostJsonAsync(new {ImageUrl = imageUrl, Title = title})
                .ReceiveJson<GeneratedImage>();
            return result.FileKey;
        }
    }
}