using EduHub.Application.Abstractions;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using Moq;

namespace EduHub.Tests;

public class EventServiceTests
{
    [Fact]
    public void CreateEvent_WithValidData_CallsAddAndSendExactlyOnce()
    {
        var repository = new Mock<IEventRepository>();
        var notifier = new Mock<INotificationService>();
        var factory = new Mock<INotificationServiceFactory>();
        factory.Setup(f => f.Create("email")).Returns(notifier.Object);

        var service = new EventService(repository.Object, factory.Object);

        var ev = service.CreateEvent(Guid.NewGuid(), "Workshop", DateTime.UtcNow.AddDays(7), 50, "email");

        Assert.Equal("Workshop", ev.Title);
        repository.Verify(r => r.Add(It.IsAny<Event>()), Times.Once);
        notifier.Verify(n => n.Send(It.IsAny<string>()), Times.Once);
    }
}
