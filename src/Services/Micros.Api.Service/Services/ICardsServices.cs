using System.Threading.Tasks;

namespace Micros.Api.Service.Services
{
    public interface ICardsServices
    {
        Task GenerateRandomCard();

        Task GenerateDogCard();

        Task GenerateCatCard();
    }
}