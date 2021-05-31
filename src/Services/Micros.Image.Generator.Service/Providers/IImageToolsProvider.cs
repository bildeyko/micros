using System.Threading.Tasks;

namespace Micros.Image.Generator.Service.Providers
{
    public interface IImageToolsProvider
    {
        Image GenerateImage(string title, byte[] imageBytes);
    }
}