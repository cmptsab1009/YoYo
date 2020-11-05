using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoYo.Provider.Mapper
{
    public static class AutoMap
    {
        static IMapper _mapper;
        public static TDest Mapping<TSource, TDest>(TSource Mapper)
        {
            MapperConfiguration configuration = new MapperConfiguration(config =>
            {
                config.CreateMap<TSource, TDest>();
            });
            _mapper = configuration.CreateMapper();
            return _mapper.Map<TSource, TDest>(Mapper);
        }
    }
}
