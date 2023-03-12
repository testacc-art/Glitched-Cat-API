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
    public class PostControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMediator> _mediator;

        public PostControllerTests()
        {
            // Initialize mapper
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            // Initialize mediator, logger and mapper mock
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task GetAllPosts_ReturnsOkResponse_WithListOfPosts()
        {
            // Arrange
            var postResponses = new PostResponse[]
                { new PostResponse { Id = Guid.NewGuid() }, new PostResponse { Id = Guid.NewGuid() } };
            var getAllPostsQuery = new GetAllPostsQuery();
            _mediator.Setup(x => x.Send(It.IsAny<GetAllPostsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(postResponses);

            var postController = new PostController(_mapper, _mediator.Object);

            // Act
            var response = await postController.GetAllPosts();

            // Assert
            var objectResult = response as ObjectResult;
            objectResult.Should().NotBeNull();
            if (objectResult != null)
            {
                objectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

                var postResponsesResult = objectResult.Value as PostResponse[];
                postResponsesResult.Should().BeEquivalentTo(postResponses);
            }
        }

        [Fact]
        public async Task GetPostById_WithValidId_ReturnsOkResponse_WithPost()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var postResponse = new PostResponse { Id = postId };
            var getPostByIdQuery = new GetPostByIdQuery { Id = postId };
            _mediator.Setup(x => x.Send(It.IsAny<GetPostByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(postResponse);

            var postController = new PostController(_mapper, _mediator.Object);

            // Act
            var response = await postController.GetPostById(postId);

            // Assert
            var objectResult = response as ObjectResult;
            objectResult.Should().NotBeNull();
            if (objectResult != null)
            {
                objectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

                var postResponseResult = objectResult.Value as PostResponse;
                postResponseResult.Should().BeEquivalentTo(postResponse);
            }
        }

        [Fact]
        public async Task GetPostById_WithInvalidId_ReturnsNotFoundResponse()
        {
            // Arrange
            var postId = Guid.NewGuid();
            _mediator.Setup(x => x.Send(It.IsAny<GetPostByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((PostResponse)null);

            var postController = new PostController(_mapper, _mediator.Object);

            // Act
            var response = await postController.GetPostById(postId);

            // Assert
            var result = response as NotFoundResult;
            result.Should().NotBeNull();
        }
        
        [Fact]
        public async Task CreatePost_WithValidPostRequest_ReturnsCreatedAtRouteResult_WithNewPostId()
        {
            // Arrange
            var postRequest = new PostRequest { Title = "Test Post", Content = "Test Content" };
            var createPostCommand = new CreatePostCommand { PostRequest = postRequest};
            var newPostId = Guid.NewGuid();
            _mediator.Setup(x => x.Send(It.IsAny<CreatePostCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(newPostId);

            var postController = new PostController(_mapper, _mediator.Object);

            // Act
            var response = await postController.CreatePost(postRequest);
            var createdAtActionResult = response.Result as CreatedAtRouteResult;

            //Assert
            // var createdAtActionResult = new CreatedAtActionResult(response.Result);
            createdAtActionResult.Should().NotBeNull();
            if (createdAtActionResult != null)
            {
                createdAtActionResult.RouteName.Should().Be(nameof(postController.GetPostById));
                createdAtActionResult.RouteValues.Should().ContainKey("id");
                createdAtActionResult.RouteValues?["id"].Should().Be(newPostId);
            }
        }
        
        [Fact]
        public async Task CreatePost_WithInvalidPostRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var postRequest = new PostRequest(); // Missing required Title and Content properties
            var postController = new PostController(_mapper, _mediator.Object);

            // Act
            var response = await postController.CreatePost(postRequest);
            var result = response.Result as BadRequestResult;

            // Assert
            result.Should().NotBeNull();
        }
        
        [Fact]
        public async Task UpdatePost_WithValidIdAndPostRequest_ReturnsNoContentResult()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var postRequest = new PostRequest { Title = "Updated Post", Content = "Updated Content" };
            var updatePostCommand = new UpdatePostCommand { Id = postId, Title = postRequest.Title, Content = postRequest.Content };
            _mediator.Setup(x => x.Send(It.IsAny<UpdatePostCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var postController = new PostController(_mapper, _mediator.Object);

            // Act
            var response = await postController.UpdatePost(postId, postRequest);

            // Assert
            var noContentResult = response as NoContentResult;
            noContentResult.Should().NotBeNull();
        }
        
        [Fact]
        public async Task DeletePost_WithValidId_ReturnsAcceptedResult()
        {
            // Arrange
            var postId = Guid.NewGuid();
            _mediator.Setup(x => x.Send(It.IsAny<DeletePostCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var postController = new PostController(_mapper, _mediator.Object);

            // Act
            var response = await postController.DeletePost(postId.ToString());

            // Assert
            var acceptedResult = response as AcceptedResult;
            acceptedResult.Should().NotBeNull();
        }
    }
} 