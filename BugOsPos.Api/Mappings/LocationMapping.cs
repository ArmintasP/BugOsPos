﻿using BugOsPos.Application.Locations;
using BugOsPos.Contracts.Locations;
using Mapster;

namespace BugOsPos.Api.Mappings;

public sealed class LocationMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetLocationByIdResult, GetLocationByIdResponse>()
            .Map(dest => dest, src => src)
            .Map(dest => dest.Id, src => src.Location.Id.Value)
            .Map(dest => dest.Name, src => src.Location.Name)
            .Map(dest => dest.Address, src => src.Location.Address)
            .Map(dest => dest.Rating, src => src.Location.Rating)
            .Map(dest => dest.PhotoPaths, src => src.Location.PhotoPaths)
            .Map(dest => dest.NormalWorkingHours, src => src.Location.NormalWorkingHours)
            .Map(dest => dest.OverriddenWorkingHours, src => src.Location.OverriddenWorkingHours);
    }
}