using System.Threading.Tasks;

namespace Micros.Image.Generator.Service.Services
{
    public interface IGeneratorService
    {
        Task<string> GenerateByImageUrlAsync(string imageUrl, string title);
    }
}