using System;
using AutoMapper;
using GlitchedCat.Application.Commands;
using GlitchedCat.Domain.Common.Models.Blog;
using GlitchedCat.Domain.Entities;

namespace GlitchedCat.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostResponse>();

            CreateMap<CreatePostCommand, PostResponse>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.PostRequest.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.PostRequest.Content))
                .ForMember(dest => dest.AuthorUsername, opt => opt.Ignore())
                .ForMember(dest => dest.TotalComments, opt => opt.Ignore());

                
            CreateMap<Post, PostResponse>()
                .ForMember(dest => dest.AuthorUsername, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.TotalComments, opt => opt.MapFrom(src => src.Comments.Count));

            CreateMap<PostRequest, Post>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<Comment, CommentResponse>()
                .ForMember(dest => dest.AuthorUsername, opt => opt.MapFrom(src => src.User.Name));

            CreateMap<CommentRequest, Comment>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.PostId, opt => opt.Ignore());
            
            CreateMap<PostRequest, CreatePostCommand>()
                .ForMember(dest => dest.PostRequest, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
            
            CreateMap<CreatePostCommand, Post>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.PostRequest.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.PostRequest.Content));
            
            CreateMap<UserRequest, CreateUserCommand>()
                .ForMember(dest => dest.UserRequest, opt => opt.MapFrom(src => src));
            
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserRequest.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserRequest.Email));
            
            CreateMap<UserRequest, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<Guid, UserResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
            
            CreateMap<Guid, PostResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
        }
    }
}