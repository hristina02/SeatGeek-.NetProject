using Microsoft.AspNetCore.Mvc;

namespace SeatGeek.WebApi.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Interfaces;
    using Services.Data.Models.Statistics;

    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IEventService eventService;

        public StatisticsApiController(IEventService eventService)
        {
                this.eventService = eventService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {

                StatisticsServiceModel serviceModel =
                    await this.eventService.GetStatisticsAsync();

                return this.Ok(serviceModel);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
