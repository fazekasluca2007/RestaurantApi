
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
        public async Task<object> GetAllRendelesWithCard()
        {
            try
            {

                var rendelesek = await _context.Rendeles.Where(x=>x.Fizetesimod== "Kártya").Select(x=> x.Id).ToListAsync();
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
        public async Task<object> GetAllRendelesWithFood()
        {
            try
            {

                var rendelesek = await _context.Rendeles.Include(x=> x.Kapcsolos).ThenInclude(x=> x.Termekek).ToListAsync();
                var food = rendelesek.Select(x => new { RendelesId = x.Id, Termek = x.Kapcsolos.Select(y => new { TermekNev = y.Termekek.Etel, TermekAr = y.Termekek.Ar }) });
                
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

