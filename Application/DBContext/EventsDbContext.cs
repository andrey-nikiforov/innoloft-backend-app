using Microsoft.EntityFrameworkCore;

namespace Innoloft_Application.DBContext
{
    public class EventsDbContext : DbContext
    {
        public EventsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Event>()
            //    .HasMany(x => x.EventParticipants)
            //    .WithOne(x => x.Event)
            //    .HasForeignKey(x => x.EventId);


            //modelBuilder.Entity<User>()
            //    .HasMany(x => x.EventParticipants)
            //    .WithOne(x => x.User)
            //     .HasForeignKey(x => x.UserId);


            //modelBuilder.Entity<EventParticipant>()
            //    .HasOne(x => x.Event)
            //    .WithMany(x => x.EventParticipants)
            //    .HasForeignKey(x => x.EventId);

            //modelBuilder.Entity<EventParticipant>()
            //    .HasOne(x => x.User)
            //    .WithMany(x => x.EventParticipants)
            //    .HasForeignKey(x => x.UserId);



            modelBuilder.Entity<EventParticipant>()
            .HasIndex(c => new { c.UserId, c.EventId})
            .IsUnique(true);


            new DbInitializer(modelBuilder).Seed();
        }


        public virtual DbSet<Event> Events { get; set; }        
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<EventParticipant> Participants { get; set; }
    }

    public class DbInitializer
    {
        private readonly ModelBuilder _modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            _modelBuilder.Entity<User>().HasData(
                   new User() { Id = 1, Name = "Bruce", Surname = "Willis" }

            );

            _modelBuilder.Entity<Event>().HasData(new Event()
            {
                Id = 1,
                DateStart = DateTime.Today,
                DateEnd = DateTime.Today.AddDays(10),
                Description = "Fun Event. Please come!",
                TimeZoneId = 1,
                Title = "Fun Event"
            }
            );
        }
    }
}
