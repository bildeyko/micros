using System.Threading.Tasks;
using Micros.Api.Service.Providers;
using Microsoft.Extensions.Logging;

namespace Micros.Api.Service.Services
{
    public class CardsServices : ICardsServices
    {
        private readonly ILogger<CardsServices> _logger;
        private readonly IDogsServiceProvider _dogsServiceProvider;
        private readonly IImageGeneratorServiceProvider _imageGeneratorService;

        public CardsServices(ILogger<CardsServices> logger, IDogsServiceProvider dogsServiceProvider, IImageGeneratorServiceProvider imageGeneratorService)
        {
            _logger = logger;
            _dogsServiceProvider = dogsServiceProvider;
            _imageGeneratorService = imageGeneratorService;
        }

        public async Task GenerateRandomCard()
        {
            var fact = await _dogsServiceProvider.GetFactAsync();
            var imageUrl = await _dogsServiceProvider.GetImageUrlAsync();

            var newImageKey = await _imageGeneratorService.GenerateAsync(fact, imageUrl);
            var a = 5;
        }
    }
}