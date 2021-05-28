using System.Threading.Tasks;

namespace Micros.Funny.Service.Services
{
    public interface IFactService
    {
        Task<string> GetRandomFactAsync();
    }
}