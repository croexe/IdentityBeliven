﻿using AutoMapper;
using TaskManager.Infrastructure.MappingProfiles;

namespace TaskManager.Repository.Tests;

internal static class AutoMapperFactory
{
    public static IMapper BuildAutoMapper()
    {
        var mapper = new MapperConfiguration(x => x.AddProfile(new EntitiesProfile()))
                                            .CreateMapper();
        return mapper;
    }
}