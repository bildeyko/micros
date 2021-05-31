using System.Threading.Tasks;

namespace Micros.Api.Service.Providers
{
    public interface IImageGeneratorServiceProvider
    {
        Task<string> GenerateAsync(string title, string imageUrl);
    }
}