namespace Innoloft_Application.Dto
{
    public class EditOrCreateEventDto
    {
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Description { get; set; }
        public int TimeZoneId { get; set; }
    }
}
