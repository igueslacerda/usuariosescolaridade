namespace UsuariosEscolaridade.Helpers;

using AutoMapper;
using UsuariosEscolaridade.Entities;
using UsuariosEscolaridade.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<EscolaridadeEntity, EscolaridadeModel>()
            .ForAllMembers(p => p.Condition(
                (src, dest, prop) => 
                {
                    // ignorar propriedades nulas e strings vazias
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
    }
}