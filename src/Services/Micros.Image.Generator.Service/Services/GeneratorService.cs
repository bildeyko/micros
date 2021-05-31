using System;
using System.Threading.Tasks;
using Micros.Image.Generator.Service.Providers;
using Micros.Image.Generator.Service.Tools;

namespace Micros.Image.Generator.Service.Services
{
    public class GeneratorService : IGeneratorService
    {
        private readonly IImageToolsProvider _imageTools;
        private readonly IStorageProvider _storage;
        private readonly IDownloader _downloader;

        public GeneratorService(IImageToolsProvider imageTools, IStorageProvider storage, IDownloader downloader)
        {
            _imageTools = imageTools;
            _storage = storage;
            _downloader = downloader;
        }

        public async Task<string> GenerateByImageUrlAsync(string imageUrl, string title)
        {
            var mainImage = await _downloader.DownloadAsByteArrayAsync(imageUrl);

            var resultImage = _imageTools.GenerateImage(title, mainImage);
            var key = _storage.Save(resultImage.Data, $"{DateTime.Now:s}");

            return key;
        }


    }
}