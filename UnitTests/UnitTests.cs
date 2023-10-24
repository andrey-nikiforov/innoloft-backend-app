
using Innoloft_Application;
using Innoloft_Application.Controllers;
using Innoloft_Application.DBContext;
using Innoloft_Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests
{
    public class UnitTests
    {
        [Fact]
        public async Task ShouldThrowException_EventNotFound()
        {
            EventsService service = GetEventsService();
            await Assert.ThrowsAsync<EventNotFoundException>(async () => await service.GetEventByIdAsync(1));
        }

        [Fact]
        public async Task ShouldReturn_EventObject()
        {
            EventsService service = GetEventsService();
            Event ev = await service.GetEventByIdAsync(2);
            Assert.NotNull(ev);
        }

        [Fact]
        public async Task ShouldReturnNotFound()
        {
            EventsController controller = GetEventsController();

            IActionResult result = await controller.GetEvent(1);
            Assert.True(result is NotFoundObjectResult);
        }


        private static EventsService GetEventsService()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventsDbContext>();
            Mock<EventsDbContext> dbContextMock = new Mock<EventsDbContext>(optionsBuilder.Options);
            dbContextMock.Setup(x => x.Events.FindAsync(It.IsIn(2)).Result).Returns(new Event());
            dbContextMock.Setup(x => x.Events.FindAsync(It.IsIn(1)).Result).Returns((Event)null);

            EventsService eventsService = new(dbContextMock.Object);  
            return eventsService;            
        }

        private static EventsController GetEventsController()
        {
            EventsService service = GetEventsService();
            Mock<ILogger<EventsController>> loggerMock = new Mock<ILogger<EventsController>>();
            EventsController eventsController = new(loggerMock.Object, service);

            return eventsController;
        }
    }
}