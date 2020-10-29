using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechLibrary.Test.Common
{
    public static class AutoMapperUtility
    {
        static IMapper _mapper;

        public static IMapper CreateAutoMapper()
        {
            if (_mapper == null)
            {
                MapperConfiguration mapperConfiguration;

                mapperConfiguration = new MapperConfiguration(cfg =>
                    cfg.AddMaps(
                        "TechLibrary"
                    )
                );

                mapperConfiguration.CompileMappings();
                _mapper = mapperConfiguration.CreateMapper();
            }
            return _mapper;
        }
    }
}
