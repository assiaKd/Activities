using Activities.Application.Interfaces;
using Activities.Domain;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Activities.Application.Activities.Commands
{
    public class EditActivity
    {
        public class Command : IRequest<Unit>
        {
            public required Activity ActivityDto { get; set; }
        }

        public class Handler(IActivityRepository activityRepository, IMapper mapper) : IRequestHandler<Command, Unit>
        {
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = mapper.Map<Activity>(request.ActivityDto);
                await activityRepository.EditActivity(activity,cancellationToken);
                return Unit.Value;
            }
        }
    }
}
