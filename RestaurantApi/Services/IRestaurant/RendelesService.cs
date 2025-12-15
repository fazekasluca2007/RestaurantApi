
using EtteremApi.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;
using RestaurantApi.Models.Dtos;

namespace RestaurantApi.Services.IRestaurant
{
    public class RendelesService : IRendeles
    {
        private readonly EtteremContext _context;
        resultResponseDto resultResponseDto=new resultResponseDto();
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
        public async Task<object> GetAllRendelesWithCola()
        {
            try
            {
                var rendelesek = await _context.Rendeles
                    .Include(r => r.Kapcsolos)
                        .ThenInclude(k => k.Termekek)
                    .Where(r => r.Kapcsolos.Any(k => k.Termekek.Etel == "Kóla"))
                    .Select(r => new
                    {
                        RendelesId = r.Id,
                        Termekek = r.Kapcsolos
                            .Where(k => k.Termekek.Etel == "Kóla")
                            .Select(k => new
                            {
                                TermekNev = k.Termekek.Etel,
                                TermekAr = k.Termekek.Ar
                            })
                    })
                    .ToListAsync();

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
        public async Task<object> GetRendelesTetelLista()
        {
            try
            {
                var lista = await (
                    from r in _context.Rendeles
                    join k in _context.Kapcsolos on r.Id equals k.RendelesId
                    join t in _context.Termekeks on k.TermekekId equals t.Id
                    orderby r.Id   
                    select new RendelesTetelListaDto
                    {
                        RendelesId = r.Id,
                        TermekNev = t.Etel,
                        Ar = (int)t.Ar
                    }
                ).ToListAsync();

                resultResponseDto.message = "Sikeres lekérdezés";
                resultResponseDto.result = lista;

                return resultResponseDto;
            }
            catch (Exception ex)
            {
                resultResponseDto.message = ex.Message;
                resultResponseDto.result = null;
                return resultResponseDto;
            }
        }
        public async Task<object> GetTermekRendelesLegalabbEgyszer()
        {
            try
            {
                var lista = await (
                    from r in _context.Rendeles
                    join k in _context.Kapcsolos on r.Id equals k.RendelesId
                    join t in _context.Termekeks on k.TermekekId equals t.Id
                    group new { r, t } by new
                    {
                        r.Id,
                        t.Etel
                    }
                    into g
                    orderby g.Key.Id
                    select new TermekRendelesDto
                    {
                        RendelesId = g.Key.Id,
                        TermekNev = g.Key.Etel
                    }
                ).ToListAsync();

                resultResponseDto.message = "Sikeres lekérdezés";
                resultResponseDto.result = lista;

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

