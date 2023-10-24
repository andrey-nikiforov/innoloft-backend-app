using System.ComponentModel.DataAnnotations;

namespace Innoloft_Application.DBContext
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Description { get; set; }
        public int TimeZoneId { get; set; }


        public List<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
    }
}
