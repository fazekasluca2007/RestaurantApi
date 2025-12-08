using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models.Dtos;
using RestaurantApi.Services.IRestaurant;
 

namespace RestaurantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RendelesController : ControllerBase
    {
        private readonly IRendeles _rendeles;
        public RendelesController(IRendeles rendeles)
        {
            _rendeles = rendeles;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllRendeles()
        {
            var rendeles = await _rendeles.GetAllRendeles();
            if (rendeles != null)
            {
                return Ok(rendeles);
            }
            return BadRequest(rendeles);

        }
        [HttpGet("withcard")]
        public async Task<ActionResult> GetAllRendelesWithCard()
        {
            var rendeles = await _rendeles.GetAllRendelesWithCard();
            if (rendeles != null)
            {
                return Ok(rendeles);
            }
            return BadRequest(rendeles);

        }
        [HttpGet("withfood")]
        public async Task<ActionResult> GetAllRendelesWithFood()
        {
            var rendeles = await _rendeles.GetAllRendelesWithFood();
            if (rendeles != null)
            {
                return Ok(rendeles);
            }
            return BadRequest(rendeles);

        }
    }
}
