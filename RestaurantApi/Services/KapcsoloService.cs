using RestaurantApi.Models;
using RestaurantApi.Models.Dtos;
using RestaurantApi.Services.IRestaurant;

namespace RestaurantApi.Services
{
    public class KapcsoloService : IKapcsolo
    {
        private readonly EtteremContext _context;
        private readonly resultResponseDto resultResponseDto;
     
        public KapcsoloService(EtteremContext context,resultResponseDto resultResponseDTO)
        {
            _context = context;
            resultResponseDto = resultResponseDTO;

        }
        public async Task<object> PostNewRelation(AddRelationDto addRelationDto)
        {
            try
            {
                var relation = new Kapcsolo
                {
                    RendelesId=addRelationDto.RendelesId,
                    TermekekId=addRelationDto.TermekekId,
                };
                if (relation != null)
                {
                    await _context.Kapcsolos.AddAsync(relation);
                    await _context.SaveChangesAsync();
                    resultResponseDto.message = "Sikeres összrendelés";
                    resultResponseDto.result = relation;
                    return resultResponseDto;
                }
                resultResponseDto.message = "Sikertelen összrendelés";
                resultResponseDto.result = relation;
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
