using System.Threading.Tasks;

namespace Micros.Image.Generator.Service.Tools
{
    public interface IDownloader
    {
        Task<byte[]> DownloadAsByteArrayAsync(string url);
    }
}