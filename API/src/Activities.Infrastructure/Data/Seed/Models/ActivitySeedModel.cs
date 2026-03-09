
namespace Activities.Infrastructure.Data.Seed.Models
{
    public class ActivitySeedModel
    {
        public string Title { get; set; } = "";
        public int DateOffsetMonths { get; set; }
        public string Description { get; set; } = "";
        public string Category { get; set; } = "";
        public string City { get; set; } = "";
        public string Venue { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<ActivityAttendeeSeedModel> Attendees { get; set; } = [];
    }
}
