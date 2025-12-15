
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;
using RestaurantApi.Models.Dtos;

namespace RestaurantApi.Services.IRestaurant
{
    public class TermekService : ITermek
    {
        private readonly EtteremContext _context;
        resultResponseDto resultResponseDto = new resultResponseDto();
        public TermekService(EtteremContext context)
        {
            _context = context;
        }
        public async Task<object> GetTermek()
        {
            try
            {

                var termekek = await _context.Termekeks.Select(x => new { x.Etel, x.Ar }).ToListAsync();
                    
                   
                resultResponseDto.message = "Sikeres lekérdezés";
                resultResponseDto.result = termekek;
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

