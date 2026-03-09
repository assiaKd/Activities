using Activities.Application.Activities.Commands;
using Activities.Application.Interfaces;
using Activities.Domain;
using FluentAssertions;
using Moq;

namespace Activities.Application.Tests
{
    public class CreateActivityTests
    {
        private readonly Mock<IActivityRepository> _repositoryMock;

        public CreateActivityTests()
        {
            _repositoryMock = new Mock<IActivityRepository>();
        }

        [Fact]
        public async Task Handle_ShouldCreateActivity_AndReturnId()
        {
            // Arrange
            var activity = new Activity
            {
                Id = "123",
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
                .Setup(x => x.CreateActivity(activity, It.IsAny<CancellationToken>()))
                .ReturnsAsync("123");

            var handler = new CreateActivity.Handler(_repositoryMock.Object);

            var command = new CreateActivity.Command
            {
                Activity = activity
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be("123");

            _repositoryMock.Verify(
                x => x.CreateActivity(activity, It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCallRepository_WithCorrectActivity()
        {
            // Arrange
            var activity = new Activity
            {
                Id = "456",
                Title = "Swimming",
                Description = "Pool training",
                Category = "Sport",
                City = "Montreal",
                Venue = "Olympic Pool",
                Date = DateTime.UtcNow,
                Latitude = 45.55,
                Longitude = -73.56
            };

            _repositoryMock
                .Setup(x => x.CreateActivity(It.IsAny<Activity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(activity.Id);

            var handler = new CreateActivity.Handler(_repositoryMock.Object);

            var command = new CreateActivity.Command
            {
                Activity = activity
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(
                x => x.CreateActivity(It.Is<Activity>(a => a.Id == "456"), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
