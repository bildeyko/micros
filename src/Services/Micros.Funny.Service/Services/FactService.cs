using System.Threading.Tasks;
using Micros.Funny.Service.Providers;

namespace Micros.Funny.Service.Services
{
    public class FactService : IFactService
    {
        private readonly IFactProvider _factProvider;

        public FactService(IFactProvider factProvider)
        {
            _factProvider = factProvider;
        }

        public async Task<string> GetRandomFactAsync()
        {
            return await _factProvider.GetFactAsync();
        }
    }
}