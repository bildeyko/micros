using System;
using System.IO;
using System.Threading.Tasks;
using Micros.Image.Generator.Service.Providers;
using Micros.Image.Generator.Service.Tools;
using Microsoft.Extensions.Logging;

namespace Micros.Image.Generator.Service.Services
{
    public class GeneratorService : IGeneratorService
    {
        private readonly ILogger<GeneratorService> _logger;
        private readonly IImageToolsProvider _imageTools;
        private readonly IStorageProvider _storage;
        private readonly IDownloader _downloader;

        public GeneratorService(ILogger<GeneratorService> logger, 
            IImageToolsProvider imageTools, 
            IStorageProvider storage, 
            IDownloader downloader)
        {
            _logger = logger;
            _imageTools = imageTools;
            _storage = storage;
            _downloader = downloader;
        }

        public async Task<string> GenerateByImageUrlAsync(string imageUrl, string title)
        {
            byte[] mainImage;
            try
            {
                mainImage = await _downloader.DownloadAsByteArrayAsync(imageUrl);
                
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Can't download an image.");
                //throw;

                mainImage = await File.ReadAllBytesAsync("./Assets/Images/loading_cat.jpg");
            }

            var resultImage = _imageTools.GenerateImage(title, mainImage);
            var key = _storage.Save(resultImage.Data, $"{DateTime.Now:s}");

            return key;
        }


    }
}