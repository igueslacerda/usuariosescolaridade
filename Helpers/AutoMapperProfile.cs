namespace UsuariosEscolaridade.Helpers;

using AutoMapper;
using UsuariosEscolaridade.Entities;
using UsuariosEscolaridade.Models;
using UsuariosEscolaridade.Services.Requests;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<EscolaridadeEntity, EscolaridadeModel>()
            .ForAllMembers(p => p.Condition((src, dest, prop) => IgnoreNullOnMap(prop)));

        CreateMap<UsuarioEntity, UsuarioModel>()
            .ForAllMembers(p => p.Condition((src, dest, prop) => IgnoreNullOnMap(prop)));

        CreateMap<UsuarioNovoRequest, UsuarioEntity>()
            .ForMember(dest => dest.Escolaridade, act => act.Ignore());

        CreateMap<UsuarioAlterarRequest, UsuarioEntity>()
            .ForMember(dest => dest.Escolaridade, act => act.Ignore())
            .ForAllMembers(p => p.Condition((src, dest, prop) => IgnoreNullOnMap(prop)));
    }

    private bool IgnoreNullOnMap(object prop)
    {
        // ignorar propriedades nulas e strings vazias
        if (prop == null) return false;
        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

        return true;
    }
}