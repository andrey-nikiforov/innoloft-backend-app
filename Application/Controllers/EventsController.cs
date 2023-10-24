using Innoloft_Application.Dto;
using Innoloft_Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Innoloft_Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {

        private readonly ILogger<EventsController> _logger;
        private readonly EventsService _eventsService;

        public EventsController(ILogger<EventsController> logger, EventsService eventsService)
        {
            _logger = logger;
            _eventsService = eventsService;
        }


        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] int take, [FromQuery] int lastId)
        {
            try
            {
                return Ok(await _eventsService.GetAllEventsPageableAsync(take, lastId));
            }
            catch (EventNotFoundException ex)
            {
                return new NotFoundObjectResult(ex.InnerException);
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            try
            {
                return Ok(await _eventsService.GetEventByIdAsync(id));
            }
            catch (EventNotFoundException ex)
            {
                return new NotFoundObjectResult(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventAsync(EditOrCreateEventDto dto)
        {
            return Ok(await _eventsService.CreateEventAsync(dto));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditEventAsync(int id, EditOrCreateEventDto dto)
        {
            await _eventsService.EditEventAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEventAsync(int id)
        {
            try
            {
                await _eventsService.DeleteEventAsync(id);
                return Ok();
            }
            catch (EventNotFoundException ex)
            {
                return new NotFoundObjectResult(ex.InnerException);
            }
        }

        [HttpPost("participate")]
        public async Task<IActionResult> ParticipateInEvent(EventParticipationDto eventParticipationDto)
        {
            if (!eventParticipationDto.IsConsent)
            {
                return new BadRequestObjectResult("You should approve participation consent");
            }

            // hardcoded as we don't have an auth service
            const int userId = 1;

            await _eventsService.ParticipateInEvent(userId, eventParticipationDto.EventId);
            return Ok();

        }
    }
}