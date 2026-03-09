using Activities.Application.Interfaces;
using Activities.Domain;
using MediatR;


namespace Activities.Application.Activities.Queries
{
    public class GetActivityDetails
    {
        public class Query : IRequest<Activity>
       {
            public required string Id { get; set; }
        }

        public class Handler(IActivityRepository activityRepository) : IRequestHandler<Query, Activity>
        {
            public async Task<Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                return await activityRepository.GetActivityDetailsAsync(request.Id,cancellationToken)??throw new Exception("activity not found");
            }
        }
    }
}
