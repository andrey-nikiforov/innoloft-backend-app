using Innoloft_Application.DBContext;
using Innoloft_Application.Dto;
using System.Data.Entity;

namespace Innoloft_Application.Services
{
    public class EventsService
    {
        private readonly EventsDbContext _eventsDbContext;

        public EventsService(EventsDbContext eventsDbContext)
        {
            _eventsDbContext = eventsDbContext;
        }

        public async Task<EventDto[]> GetAllEventsPageableAsync(int take, int lastId)
        {
            Event[] events = _eventsDbContext.Events
                .Include(x=> x.EventParticipants)
                .OrderBy(x => x.DateStart)
                .Where(x => x.Id > lastId)
                .Take(take)
                .ToArray();

            EventDto[] eventDtos = events
                .Select(EventDto.FromEntity)
                .ToArray();


            return eventDtos;
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await GetEventAsync(id);
        }


        public async Task<int> CreateEventAsync(EditOrCreateEventDto dto)
        {
            Event newEvent = new()
            {
                Title = dto.Title,
                DateStart = dto.DateStart,
                DateEnd = dto.DateEnd,
                Description = dto.Description,
                TimeZoneId = dto.TimeZoneId,
            };

            await _eventsDbContext.AddAsync(newEvent);
            await _eventsDbContext.SaveChangesAsync();

            return newEvent.Id;
        }

        public async Task EditEventAsync(int id, EditOrCreateEventDto dto)
        {
            Event ev = await GetEventAsync(id);

            ev.Title = dto.Title;
            ev.DateStart = dto.DateStart;   
            ev.DateEnd = dto.DateEnd;
            ev.Description = dto.Description;
            await _eventsDbContext.SaveChangesAsync();                        
        }

        public async Task DeleteEventAsync(int id)
        {
            Event ev = await GetEventAsync(id);
            _eventsDbContext.Events.Remove(ev);
            await _eventsDbContext.SaveChangesAsync();
        }


        public async Task ParticipateInEvent(int userId, int eventId)
        {

           


            var user = await _eventsDbContext.Users.FindAsync(userId);            
            var ev = await _eventsDbContext.Events.FindAsync(eventId) ?? throw new EventNotFoundException(eventId);

            EventParticipant participant = _eventsDbContext.Participants.FirstOrDefault(x => x.User == user && x.Event == ev);

            if (participant != null)
            {
                return;
            }
            participant = new EventParticipant() { User = user, Event = ev };

            await _eventsDbContext.Participants.AddAsync(participant);
            await _eventsDbContext.SaveChangesAsync();

        } 

        private async Task<Event> GetEventAsync(int id)
        {
            Event ev = await _eventsDbContext.Events
                .FindAsync(id) ?? throw new EventNotFoundException(id);
            return ev;
        }

        

    }
}
