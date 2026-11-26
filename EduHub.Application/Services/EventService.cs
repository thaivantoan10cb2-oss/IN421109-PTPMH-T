using EduHub.Application.Abstractions;
using EduHub.Domain.Entities;

namespace EduHub.Application.Services;

/// <summary>
/// Application service for events. It only coordinates work: validation lives in
/// <see cref="Event"/>, storage behind <see cref="IEventRepository"/>, and
/// notifications behind <see cref="INotificationService"/>.
/// </summary>
public class EventService
{
    private readonly IEventRepository _repository;
    private readonly INotificationServiceFactory _notificationFactory;

    public EventService(IEventRepository repository, INotificationServiceFactory notificationFactory)
    {
        _repository = repository;
        _notificationFactory = notificationFactory;
    }

    public Event CreateEvent(Guid clubId, string title, DateTime date, int capacity, string channel)
    {
        // Resolve the channel first so an unsupported channel fails before anything is saved.
        var notifier = _notificationFactory.Create(channel);

        var ev = Event.Create(clubId, title, date, capacity);
        _repository.Add(ev);
        notifier.Send($"New event: {ev.Title}");

        return ev;
    }

    public IReadOnlyList<Event> GetEventsByClub(Guid clubId) => _repository.GetByClubId(clubId);

    public Event? GetEvent(Guid id) => _repository.GetById(id);

    public bool DeleteEvent(Guid id) => _repository.Remove(id);
}
