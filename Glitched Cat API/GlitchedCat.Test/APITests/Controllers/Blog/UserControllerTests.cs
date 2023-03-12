using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using GlitchedCat.API.Controllers.Blog;
using GlitchedCat.Application.Commands;
using GlitchedCat.Application.Mapping;
using GlitchedCat.Application.Queries.Blog;
using GlitchedCat.Domain.Common.Models.Blog;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GlitchedCat.Test.APITests.Controllers.Blog
{
    public class UserControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ILogger<UserController>> _logger;
        private readonly Mock<IMapper> _mapperMock;

        public UserControllerTests()
        {
            // Initialize mapper
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            // Initialize mediator, logger and mapper mock
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<UserController>>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOkResponse_WithListOfUsers()
        {
            // Arrange
            var userResponses = new UserResponse[]
                { new UserResponse { Id = Guid.NewGuid() }, new UserResponse { Id = Guid.NewGuid() } };
            var getAllUsersQuery = new GetAllUsersQuery();
            _mediator.Setup(x => x.Send(It.IsAny<GetAllUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userResponses);

            var userController = new UserController(_mapper, _mediator.Object);

            // Act
            var response = await userController.GetAllUsers();

            // Assert
            var objectResult = response as ObjectResult;
            objectResult.Should().NotBeNull();
            if (objectResult != null)
            {
                objectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

                var userResponsesResult = objectResult.Value as UserResponse[];
                userResponsesResult.Should().BeEquivalentTo(userResponses);
            }
        }

        [Fact]
        public async Task GetUserById_WithValidId_ReturnsOkResponse_WithUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userResponse = new UserResponse { Id = userId };
            var getUserByIdQuery = new GetUserByIdQuery { Id = userId };
            _mediator.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userResponse);

            var userController = new UserController(_mapper, _mediator.Object);

            // Act
            var response = await userController.GetUserById(userId);

            // Assert
            var objectResult = response as ObjectResult;
            objectResult.Should().NotBeNull();
            if (objectResult != null)
            {
                objectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

                var userResponseResult = objectResult.Value as UserResponse;
                userResponseResult.Should().BeEquivalentTo(userResponse);
            }
        }

        [Fact]
        public async Task GetUserById_WithInvalidId_ReturnsNotFoundResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mediator.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((UserResponse)null);

            var userController = new UserController(_mapper, _mediator.Object);

            // Act
            var response = await userController.GetUserById(userId);

            // Assert
            var result = response as NotFoundResult;
            result.Should().NotBeNull();
        }
        
        [Fact]
        public async Task CreateUser_WithValidUserRequest_ReturnsCreatedAtAction_WithNewUserId()
        {
            // Arrange
            var userRequest = new UserRequest { Name = "Test User", Email = "test@email.com"};
            var createUserCommand = new CreateUserCommand { UserRequest = userRequest };
            var newUserId = Guid.NewGuid();
            _mediator.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(newUserId);

            var userController = new UserController(_mapper, _mediator.Object);

            // Act
            var response = await userController.CreateUser(userRequest);
            var createdAtActionResult = response as CreatedAtActionResult;

            // Assert
            createdAtActionResult.Should().NotBeNull();
            if (createdAtActionResult != null)
            {
                createdAtActionResult.ActionName.Should().Be(nameof(userController.GetUserById));
                createdAtActionResult.RouteValues.Should().ContainKey("id");
                createdAtActionResult.RouteValues?["id"].Should().Be(newUserId);
            }
        }
        
        [Fact]
        public async Task CreateUser_WithInvalidUserRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var userRequest = new UserRequest(); // Missing required Username property
            var userController = new UserController(_mapper, _mediator.Object);

            // Act
            var response = await userController.CreateUser(userRequest);
            var result = response as BadRequestResult;

            // Assert
            result.Should().NotBeNull();
        }
        
        [Fact]
        public async Task UpdateUser_WithValidIdAndUserRequest_ReturnsNoContentResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userRequest = new UserRequest { Name = "Updated User" };
            var updateUserCommand = new UpdateUserCommand { Id = userId, UserRequest = userRequest };
            _mediator.Setup(x => x.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var userController = new UserController(_mapper, _mediator.Object);

            // Act
            var response = await userController.UpdateUser(userId, userRequest);

            // Assert
            var noContentResult = response as NoContentResult;
            noContentResult.Should().NotBeNull();
        }
    }
}
