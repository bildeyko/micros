using System;
using System.Threading.Tasks;
using Flurl.Http;

namespace Micros.Image.Generator.Service.Tools
{
    public class Downloader : IDownloader
    {
        public async Task<byte[]> DownloadAsByteArrayAsync(string url)
        {
            return await url.ConfigureRequest(c => c.Timeout = new TimeSpan(0, 0, 10)).GetBytesAsync();
        }
    }
}