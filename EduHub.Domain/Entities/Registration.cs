namespace EduHub.Domain.Entities;

/// <summary>
/// Event registration. Business rules:
/// - a new registration is Pending;
/// - only a Pending registration can be confirmed;
/// - Pending or Confirmed registrations can be cancelled;
/// - a Cancelled registration can no longer change.
/// </summary>
public class Registration
{
    public Guid Id { get; private set; }
    public Guid MemberId { get; private set; }
    public Guid EventId { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public RegistrationStatus Status { get; private set; }

    private Registration(Guid memberId, Guid eventId)
    {
        Id = Guid.NewGuid();
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = DateTime.UtcNow;
        Status = RegistrationStatus.Pending;
    }

    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
            throw new ArgumentException("MemberId must not be empty.", nameof(memberId));

        if (eventId == Guid.Empty)
            throw new ArgumentException("EventId must not be empty.", nameof(eventId));

        return new Registration(memberId, eventId);
    }

    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
            throw new InvalidOperationException(
                $"Only a Pending registration can be confirmed (current status: {Status}).");

        Status = RegistrationStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
            throw new InvalidOperationException("A Cancelled registration cannot be changed.");

        Status = RegistrationStatus.Cancelled;
    }
}
