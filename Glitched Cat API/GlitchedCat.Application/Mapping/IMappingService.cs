using System.Threading.Tasks;
using AutoMapper;

namespace GlitchedCat.Application.Mapping
{
    public interface IMappingService
    {
        Task<TDestination> MapAsync<TSource, TDestination>(TSource source);
    }

    public class MappingService : IMappingService
    {
        private readonly IMapper _mapper;

        public MappingService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<TDestination> MapAsync<TSource, TDestination>(TSource source)
        {
            return await Task.Run(() => _mapper.Map<TSource, TDestination>(source));
        }
    }
}