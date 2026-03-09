

using Activities.Domain;

namespace Activities.Application.Interfaces
{
    public interface IActivityRepository
    {
        Task<Activity?> GetActivityDetailsAsync(string id, CancellationToken cancellationToken);
        Task<List<Activity>> GetActivityListAsync(CancellationToken token);
        Task<string> CreateActivity(Activity activity, CancellationToken cancellationToken);
        Task DeleteActivity(string id, CancellationToken cancellationToken);
        Task EditActivity(Activity activity, CancellationToken cancellationToken);
    }
}
