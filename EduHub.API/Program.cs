using EduHub.Application.Abstractions;
using EduHub.Application.Services;
using EduHub.Infrastructure.Notifications;
using EduHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Singleton: the repository keeps data in a List, so ONE instance must live for the whole
// application. With AddScoped each request would get a new empty list.
builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();

// Both implementations are registered as INotificationService; the factory receives
// IEnumerable<INotificationService> and picks one by channel.
builder.Services.AddSingleton<INotificationService, EmailNotificationService>();
builder.Services.AddSingleton<INotificationService, SmsNotificationService>();
builder.Services.AddSingleton<INotificationServiceFactory, NotificationServiceFactory>();

builder.Services.AddScoped<EventService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
