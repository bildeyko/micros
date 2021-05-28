using System.Threading.Tasks;

namespace Micros.Funny.Service.Services
{
    public interface IImageService
    {
        Task<string> GetRandomImageUrlAsync();
    }
}