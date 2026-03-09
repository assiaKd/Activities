using Activities.Application.Interfaces;
using Activities.Domain;
using MediatR;

namespace Activities.Application.Activities.Commands
{
    public class CreateActivity
    {
        public class Command : IRequest<string>
        {
            public required Activity Activity { get; set; }
        }

        public class Handler(IActivityRepository activityRepository) : IRequestHandler<Command, string>
        {
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
              return  await activityRepository.CreateActivity(request.Activity, cancellationToken);
            }
        }
    }
}
