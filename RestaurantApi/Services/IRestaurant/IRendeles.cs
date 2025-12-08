using RestaurantApi.Models.Dtos;
namespace RestaurantApi.Services.IRestaurant
{
    public interface IRendeles
    {
        Task<object> GetAllRendeles();
        Task<object?> GetAllRendelesWithCard();
    }
}
