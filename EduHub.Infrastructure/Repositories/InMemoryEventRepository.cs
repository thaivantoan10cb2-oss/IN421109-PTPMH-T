using EduHub.Application.Abstractions;
using EduHub.Domain.Entities;

namespace EduHub.Infrastructure.Repositories;

/// <summary>
/// Keeps events in an instance List. It MUST be registered as a Singleton:
/// with AddScoped every HTTP request would get a new, empty list.
/// </summary>
public class InMemoryEventRepository : IEventRepository
{
    private readonly List<Event> _events = new();
    private readonly object _lock = new();

    public void Add(Event ev)
    {
        lock (_lock)
        {
            _events.Add(ev);
        }
    }

    public Event? GetById(Guid id)
    {
        lock (_lock)
        {
            return _events.FirstOrDefault(e => e.Id == id);
        }
    }

    public IReadOnlyList<Event> GetByClubId(Guid clubId)
    {
        lock (_lock)
        {
            return _events.Where(e => e.ClubId == clubId).ToList();
        }
    }

    public bool Remove(Guid id)
    {
        lock (_lock)
        {
            return _events.RemoveAll(e => e.Id == id) > 0;
        }
    }
}
