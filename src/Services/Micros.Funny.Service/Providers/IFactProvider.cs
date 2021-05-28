using System.Threading.Tasks;

namespace Micros.Funny.Service.Providers
{
    public interface IFactProvider
    {
        Task<string> GetFactAsync();
    }
}