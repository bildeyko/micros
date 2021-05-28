using System.Threading.Tasks;

namespace Micros.Funny.Service.Providers
{
    public interface IImageProvider
    {
        Task<string> GetImageUrlAsync();
    }
}