namespace Innoloft_Application
{
    public class EventNotFoundException : Exception
    {
        public EventNotFoundException(int eventId) : base ($"Event with id={eventId} not found") { }
    }
}
