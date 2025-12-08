using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models.Dtos;
using RestaurantApi.Services.IRestaurant;


namespace RestaurantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermekController : ControllerBase
    {
        private readonly ITermek _termekek;
        public TermekController(ITermek termekek)
        {
            _termekek = termekek;
        }
        [HttpGet]
        public async Task<ActionResult> GetTermek()
        {
            var termekek = await _termekek.GetTermek();
            if (termekek != null)
            {
                return Ok(termekek);
            }
            return BadRequest(termekek);

        }
    }
}
