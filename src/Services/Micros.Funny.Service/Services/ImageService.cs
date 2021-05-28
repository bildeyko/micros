using System.Threading.Tasks;
using Micros.Funny.Service.Providers;

namespace Micros.Funny.Service.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageProvider _imageProvider;

        public ImageService(IImageProvider imageProvider)
        {
            _imageProvider = imageProvider;
        }

        public async Task<string> GetRandomImageUrlAsync()
        {
            return await _imageProvider.GetImageUrlAsync();
        }
    }
}