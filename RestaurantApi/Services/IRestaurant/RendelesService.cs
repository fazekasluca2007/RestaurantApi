
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;
using RestaurantApi.Models.Dtos;

namespace RestaurantApi.Services.IRestaurant
{
    public class RendelesService : IRendeles
    {
        private readonly EtteremContext _context;
        ResultResponseDto resultResponseDto=new ResultResponseDto();
        public RendelesService(EtteremContext context)
        {
            _context = context;
        }
        public async Task<object> GetAllRendeles()
        {
            try
            {

                var rendelesek = await _context.Rendeles.ToListAsync();
                resultResponseDto.message = "Sikeres lekérdezés";
                resultResponseDto.result = rendelesek;
                return resultResponseDto;
            }

            catch (Exception ex)
            {

                resultResponseDto.message = ex.Message;
                resultResponseDto.result = null;
                return resultResponseDto;
            }
        }
    }
}

