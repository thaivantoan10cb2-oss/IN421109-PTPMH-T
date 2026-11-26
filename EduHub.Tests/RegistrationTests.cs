using EduHub.Domain.Entities;

namespace EduHub.Tests;

public class RegistrationTests
{
    private readonly Guid _memberId = Guid.NewGuid();
    private readonly Guid _eventId = Guid.NewGuid();

    [Fact]
    public void Create_WithValidIds_ReturnsPendingRegistration()
    {
        var r = Registration.Create(_memberId, _eventId);

        Assert.Equal(RegistrationStatus.Pending, r.Status);
        Assert.Equal(_memberId, r.MemberId);
        Assert.Equal(_eventId, r.EventId);
        Assert.NotEqual(Guid.Empty, r.Id);
    }

    [Fact]
    public void Create_WithEmptyMemberId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Registration.Create(Guid.Empty, _eventId));
    }

    [Fact]
    public void Create_WithEmptyEventId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Registration.Create(_memberId, Guid.Empty));
    }

    [Fact]
    public void Confirm_WhenPending_SetsStatusToConfirmed()
    {
        var r = Registration.Create(_memberId, _eventId);

        r.Confirm();

        Assert.Equal(RegistrationStatus.Confirmed, r.Status);
    }

    [Fact]
    public void Confirm_WhenAlreadyConfirmed_ThrowsInvalidOperationException()
    {
        var r = Registration.Create(_memberId, _eventId);
        r.Confirm();

        Assert.Throws<InvalidOperationException>(() => r.Confirm());
        Assert.Equal(RegistrationStatus.Confirmed, r.Status);
    }

    [Fact]
    public void Cancel_WhenPending_SetsStatusToCancelled()
    {
        var r = Registration.Create(_memberId, _eventId);

        r.Cancel();

        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }

    [Fact]
    public void Cancel_WhenConfirmed_SetsStatusToCancelled()
    {
        var r = Registration.Create(_memberId, _eventId);
        r.Confirm();

        r.Cancel();

        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }

    [Fact]
    public void Cancel_WhenAlreadyCancelled_ThrowsInvalidOperationException()
    {
        var r = Registration.Create(_memberId, _eventId);
        r.Cancel();

        Assert.Throws<InvalidOperationException>(() => r.Cancel());
    }

    // Scenario from the exam: Create -> Cancel -> Confirm (line 3 must throw).
    [Fact]
    public void Confirm_AfterCancel_ThrowsInvalidOperationExceptionAndStatusStaysCancelled()
    {
        var r = Registration.Create(_memberId, _eventId);
        r.Cancel();

        Assert.Throws<InvalidOperationException>(() => r.Confirm());

        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }
}
