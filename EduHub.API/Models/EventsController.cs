using EduHub.API.Models;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers;

[ApiController]
public class EventsController : ControllerBase
{
    private readonly EventService _eventService;

    public EventsController(EventService eventService)
    {
        _eventService = eventService;
    }

    /// <summary>Get all events of a club.</summary>
    [HttpGet("api/clubs/{clubId:guid}/events")]
    [ProducesResponseType(typeof(IEnumerable<Event>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Event>> GetByClub(Guid clubId)
    {
        return Ok(_eventService.GetEventsByClub(clubId));
    }

    /// <summary>Get one event by id.</summary>
    [HttpGet("api/events/{id:guid}", Name = "GetEventById")]
    [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Event> GetById(Guid id)
    {
        var ev = _eventService.GetEvent(id);
        return ev is null ? NotFound() : Ok(ev);
    }

    /// <summary>Create a new event for a club.</summary>
    [HttpPost("api/clubs/{clubId:guid}/events")]
    [ProducesResponseType(typeof(Event), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<Event> Create(Guid clubId, [FromBody] CreateEventRequest request)
    {
        try
        {
            var ev = _eventService.CreateEvent(
                clubId, request.Title, request.Date, request.Capacity, request.Channel);

            return CreatedAtRoute("GetEventById", new { id = ev.Id }, ev);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid event data",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
    }

    /// <summary>Delete an event.</summary>
    [HttpDelete("api/events/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        return _eventService.DeleteEvent(id) ? NoContent() : NotFound();
    }
}
