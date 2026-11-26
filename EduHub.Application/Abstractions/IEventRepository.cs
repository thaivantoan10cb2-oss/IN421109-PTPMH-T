using EduHub.Domain.Entities;

namespace EduHub.Application.Abstractions;

public interface IEventRepository
{
    void Add(Event ev);

    Event? GetById(Guid id);

    IReadOnlyList<Event> GetByClubId(Guid clubId);

    /// <returns>true if the event existed and was removed.</returns>
    bool Remove(Guid id);
}
