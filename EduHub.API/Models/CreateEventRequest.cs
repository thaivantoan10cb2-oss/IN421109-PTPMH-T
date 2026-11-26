namespace EduHub.API.Models;

/// <param name="Title">Event title (required).</param>
/// <param name="Date">Event date and time.</param>
/// <param name="Capacity">Maximum attendees, must be greater than 0.</param>
/// <param name="Channel">Notification channel: "email" or "sms".</param>
public record CreateEventRequest(string Title, DateTime Date, int Capacity, string Channel = "email");
