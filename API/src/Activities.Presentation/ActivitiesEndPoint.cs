using Activities.Application.Activities.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
namespace Activities.Presentation
{
    public static class ActivitiesEndPoint
    {
        public static void MapActivitiesEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/activities").WithTags("Activities");

            group.MapGet("/", async (IMediator mediator) =>
            {
                var activities = await mediator.Send(new GetActivityList.Query());

                return Results.Ok(activities);
            });

            group.MapGet("/{id}", async (string id, IMediator mediator) =>
            {
                var activity = await mediator.Send(new GetActivityDetails.Query { Id=id });

                return activity != null ? Results.Ok(activity) : Results.NotFound();
            });
        }
    }
}
