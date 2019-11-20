
namespace EcoHelper.Application.Interfaces
{
    using Microsoft.EntityFrameworkCore;
    using EcoHelper.Domain.Entities;

    public interface IEcoHelperDbContext
    {
        //DbSet<Calendar> Calendars { get; set; }
        //DbSet<Event> Events { get; set; }
        //DbSet<Friends> Friends { get; set; }
        //DbSet<Location> Locations { get; set; }
        //DbSet<UserCalendar> UserCalendars { get; set; }
        //DbSet<UserEvent> UserEvents { get; set; }
        DbSet<User> Users { get; set; }
    }
}
