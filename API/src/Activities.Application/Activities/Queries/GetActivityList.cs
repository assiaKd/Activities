using Activities.Application.Interfaces;
using Activities.Domain;
using MediatR;

namespace Activities.Application.Activities.Queries;

public class GetActivityList
{
    public class Query : IRequest<List<Activity>>
    {
    }

    public class Handler(IActivityRepository activityRepository) : IRequestHandler<Query, List<Activity>>
    {
        async Task<List<Activity>> IRequestHandler<Query, List<Activity>>.Handle(Query request, CancellationToken cancellationToken)
        {
           return  await activityRepository.GetActivityListAsync(cancellationToken);
        }
    }
}