using Activities.Application.Interfaces;
using Activities.Domain;
using Activities.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Activities.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext _context;

        public ActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Activity?> GetActivityDetailsAsync(string id, CancellationToken cancellationToken)
        {
            return await _context.Activities
                                 .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Activity>> GetActivityListAsync(CancellationToken cancellationToken)
        {
            return await _context.Activities
                                 .ToListAsync();
        }

        public async Task<string> CreateActivity(Activity activity, CancellationToken cancellationToken)
        {
            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();
            return activity.Id;
        }

        public async Task DeleteActivity(string id, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity != null)
            {
                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditActivity(Activity activity, CancellationToken cancellationToken)
        {
            var existingActivity = await _context.Activities.FindAsync(activity.Id);
            if (existingActivity != null)
            {
                existingActivity.Description = activity.Description;
                existingActivity.Date = activity.Date;

                _context.Activities.Update(existingActivity);
                await _context.SaveChangesAsync();
            }
        }
    }
}