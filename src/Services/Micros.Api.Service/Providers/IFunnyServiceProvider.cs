using System.Threading.Tasks;

namespace Micros.Api.Service.Providers
{
    public interface IFunnyServiceProvider
    {
        Task<string> GetFactAsync();

        Task<string> GetImageUrlAsync();
    }
}