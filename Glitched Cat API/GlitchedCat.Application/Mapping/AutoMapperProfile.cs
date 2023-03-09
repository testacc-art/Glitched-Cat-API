using AutoMapper;
using GlitchedCat.Domain.Common.Models.Blog;
using GlitchedCat.Domain.Entities;

namespace GlitchedCat.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
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
        }
    }
}