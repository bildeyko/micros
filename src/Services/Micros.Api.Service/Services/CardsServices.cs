using System;
using System.Threading.Tasks;
using Micros.Api.Service.Providers;
using Microsoft.Extensions.Logging;

namespace Micros.Api.Service.Services
{
    public class CardsServices : ICardsServices
    {
        private readonly ILogger<CardsServices> _logger;
        private readonly IDogsServiceProvider _dogsServiceProvider;
        private readonly ICatsServiceProvider _catsServiceProvider;
        private readonly IImageGeneratorServiceProvider _imageGeneratorService;

        public CardsServices(ILogger<CardsServices> logger, 
            IDogsServiceProvider dogsServiceProvider, 
            ICatsServiceProvider catsServiceProvider,
            IImageGeneratorServiceProvider imageGeneratorService)
        {
            _logger = logger;
            _dogsServiceProvider = dogsServiceProvider;
            _catsServiceProvider = catsServiceProvider;
            _imageGeneratorService = imageGeneratorService;
        }

        public async Task GenerateRandomCard()
        {
            var random = new Random().Next(1, 2);
            if (random == 1)
            {
                await GenerateDogCard();
            }
            else
            {
                await GenerateCatCard();
            }
        }

        public async Task GenerateDogCard()
        {
            var fact = await _dogsServiceProvider.GetFactAsync();
            var imageUrl = await _dogsServiceProvider.GetImageUrlAsync();

            var newImageKey = await _imageGeneratorService.GenerateAsync(fact, imageUrl);
        }

        public async Task GenerateCatCard()
        {
            var fact = string.Empty;
            var imageUrl = string.Empty;
            try
            {
                fact = await _catsServiceProvider.GetFactAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Can't get cat's fact.");
                fact = "(⊙_⊙;)";
            }

            try
            {
                imageUrl = await _catsServiceProvider.GetImageUrlAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Can't get cat's image.");
            }

            var newImageKey = await _imageGeneratorService.GenerateAsync(fact, imageUrl);
        }
    }
}