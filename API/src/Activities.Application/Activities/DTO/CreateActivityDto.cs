public record CreateActivityDto(
    string Title,
    DateTime Date,
    string Description,
    string Category,
    string City,
    string Venue,
    double Latitude,
    double Longitude
);