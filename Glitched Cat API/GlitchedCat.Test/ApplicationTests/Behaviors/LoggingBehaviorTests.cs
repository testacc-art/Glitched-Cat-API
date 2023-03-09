using System;
using System.Threading;
using System.Threading.Tasks;
using GlitchedCat.Application.Behaviors;
using GlitchedCat.Domain.Common.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Xunit;

namespace GlitchedCat.Test.ApplicationTests.Behaviors
{
    public class LoggingBehaviorTests
    {
        [Fact]
        public async Task Handle_Should_Log_ExecutionTime()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<LoggingService>>();
            var mockLoggingService = new Mock<LoggingService>(mockLogger.Object) { CallBase = true };
            var behavior = new LoggingBehavior<object, object>(mockLoggingService.Object);
            var request = new object();
            var next = new RequestHandlerDelegate<object>(() => Task.FromResult(new object()));
            var cancellationToken = CancellationToken.None;

            // Act
            var response = await behavior.Handle(request, next, cancellationToken);

            // Assert
            mockLoggingService.Protected().Verify<Task>("LogToDataLakeAsync",
                Times.Exactly(2), // expect LogAsync to call LogToDataLakeAsync twice
                ItExpr.IsAny<string>()
            );
        }
    }
}
