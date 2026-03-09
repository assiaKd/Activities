namespace Activities.Application.Activities.DTO
{
    public record EditActivityDto(
        string Id,
        string Title,
        DateTime Date,
        string Description,
        string Category,
        string City,
        string Venue,
        double Latitude,
        double Longitude
    );
}