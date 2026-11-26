namespace EduHub.Domain.Entities;

public class Event
{
    public Guid Id { get; private set; }
    public Guid ClubId { get; private set; }
    public string Title { get; private set; }
    public DateTime Date { get; private set; }
    public int Capacity { get; private set; }

    private Event(Guid clubId, string title, DateTime date, int capacity)
    {
        Id = Guid.NewGuid();
        ClubId = clubId;
        Title = title;
        Date = date;
        Capacity = capacity;
    }

    public static Event Create(Guid clubId, string title, DateTime date, int capacity)
    {
        if (clubId == Guid.Empty)
            throw new ArgumentException("ClubId must not be empty.", nameof(clubId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title must not be empty.", nameof(title));

        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than 0.", nameof(capacity));

        return new Event(clubId, title.Trim(), date, capacity);
    }
}
