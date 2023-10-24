using System.ComponentModel.DataAnnotations;

namespace Innoloft_Application.DBContext
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
