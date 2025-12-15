using RestaurantApi.Models.Dtos;
using System.Threading.Tasks;
namespace RestaurantApi.Services.IRestaurant
{
    public interface IRendeles
    {
        Task<object> GetAllRendeles();
        Task<object> GetAllRendelesWithCard();
        Task<object> GetAllRendelesWithFood();

        Task<object> GetAllRendelesWithCola();

        Task<object> GetRendelesTetelLista();
        Task<object> GetTermekRendelesLegalabbEgyszer();
    }
}
