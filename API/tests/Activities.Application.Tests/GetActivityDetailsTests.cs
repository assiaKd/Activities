using Activities.Application.Activities.Queries;
using Activities.Application.Interfaces;
using Activities.Domain;
using FluentAssertions;
using Moq;

namespace Activities.Application.Tests
{
    public class GetActivityDetailsTests
    {
        private readonly Mock<IActivityRepository> _repositoryMock;

        public GetActivityDetailsTests()
        {
            _repositoryMock = new Mock<IActivityRepository>();
        }

        [Fact]
        public async Task Handle_ShouldReturnActivity_WhenActivityExists()
        {
            // Arrange
            var activity = new Activity
            {
                Id = "1",
                Title = "Running",
                Description = "Morning run",
                Category = "Sport",
                City = "Montreal",
                Venue = "Mont Royal",
                Date = DateTime.UtcNow,
                Latitude = 45.5,
                Longitude = -73.5
            };

            _repositoryMock
                .Setup(r => r.GetActivityDetailsAsync("1", It.IsAny<CancellationToken>()))
                .ReturnsAsync(activity);

            var handler = new GetActivityDetails.Handler(_repositoryMock.Object);

            var query = new GetActivityDetails.Query
            {
                Id = "1"
            };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be("1");
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenActivityDoesNotExist()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.GetActivityDetailsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Activity?)null);

            var handler = new GetActivityDetails.Handler(_repositoryMock.Object);

            var query = new GetActivityDetails.Query
            {
                Id = "999"
            };

            // Act
            Func<Task> act = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("activity not found");
        }
    }
}
