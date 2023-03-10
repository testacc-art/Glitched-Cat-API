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
        private readonly Mock<ILogger<PostController>> _logger;
        private readonly Mock<IMapper> _mapperMock;

        public PostControllerTests()
        {
            // Initialize mapper
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            // Initialize mediator, logger and mapper mock
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<PostController>>();
            _mapperMock = new Mock<IMapper>();
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
            objectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var postResponseResult = objectResult.Value as PostResponse;
            postResponseResult.Should().BeEquivalentTo(postResponse);
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
        public async Task CreatePost_ReturnsCreatedAtRoute_WithNewPostId()
        {
            // Arrange
            var postRequest = new PostRequest { Title = "Test Post", Content = "Test Content" };
            var command = new CreatePostCommand { PostRequest = postRequest, UserId = "testuser" };
            var postId = Guid.NewGuid();
            _mediator.Setup(x => x.Send(It.IsAny<CreatePostCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(postId);

            var expectedRouteValues = new { id = postId };
            var expectedCreatedAtRouteResult = new CreatedAtRouteResult(nameof(PostController.GetPostById), expectedRouteValues, postId);

            var postController = new PostController(_mapperMock.Object, _mediator.Object);

            // Act
            var result = await postController.CreatePost(postRequest);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
            var createdAtRouteResult = (CreatedAtRouteResult)result.Result;
            Assert.Equal(expectedRouteValues, createdAtRouteResult.RouteValues);
            Assert.Equal(postId, createdAtRouteResult.Value);

            _mediator.Verify(x => x.Send(It.IsAny<CreatePostCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

    }
} 