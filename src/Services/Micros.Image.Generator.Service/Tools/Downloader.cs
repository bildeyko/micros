using System.Threading.Tasks;
using Flurl.Http;

namespace Micros.Image.Generator.Service.Tools
{
    public class Downloader : IDownloader
    {
        public async Task<byte[]> DownloadAsByteArrayAsync(string url)
        {
            return await url.GetBytesAsync();
        }
    }
}