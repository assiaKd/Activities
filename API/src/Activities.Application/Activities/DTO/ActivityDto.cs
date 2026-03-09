namespace Activities.Application.Activities.DTO
{
    public record ActivityDto(
        string Id,
        string Title,
        DateTime Date,
        string Description,
        string Category,
        bool IsCancelled,
        string HostDisplayName,
        string HostId,
        string City,
        string Venue,
        double Latitude,
        double Longitude
    );
}