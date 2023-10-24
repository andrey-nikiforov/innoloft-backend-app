using Innoloft_Application.DBContext;

namespace Innoloft_Application.Dto
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Description { get; set; }
        public int TimeZoneId { get; set; }
        public int[]? ParticipantUsersIds { get; set; }


        public static EventDto? FromEntity(Event entity) {

            if (entity is null)
            {
                return null;
            }
            else
            {
                return new EventDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    DateStart = entity.DateStart,
                    DateEnd = entity.DateEnd,
                    Description = entity.Description,
                    TimeZoneId = entity.TimeZoneId,
                    ParticipantUsersIds = entity.EventParticipants?.Select(x => x.User.Id).ToArray()
                };
            }
        }
    }
}
