using EduHub.Domain.Entities;

namespace EduHub.Tests;

public class EventTests
{
    [Fact]
    public void Create_WithValidData_ReturnsEvent()
    {
        var clubId = Guid.NewGuid();
        var date = new DateTime(2026, 12, 1, 9, 0, 0);

        var ev = Event.Create(clubId, "Workshop", date, 30);

        Assert.NotEqual(Guid.Empty, ev.Id);
        Assert.Equal(clubId, ev.ClubId);
        Assert.Equal("Workshop", ev.Title);
        Assert.Equal(30, ev.Capacity);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyTitle_ThrowsArgumentException(string title)
    {
        Assert.Throws<ArgumentException>(() => Event.Create(Guid.NewGuid(), title, DateTime.UtcNow, 10));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Create_WithNonPositiveCapacity_ThrowsArgumentException(int capacity)
    {
        Assert.Throws<ArgumentException>(() => Event.Create(Guid.NewGuid(), "Workshop", DateTime.UtcNow, capacity));
    }
}
