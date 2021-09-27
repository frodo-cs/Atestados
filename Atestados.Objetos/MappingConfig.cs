﻿using Atestados.Datos.Modelo;
using Atestados.Objetos.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestados.Objetos
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Persona, PersonasDto>()
                .ForMember(dest => dest.PersonaID,
                    opt => opt.MapFrom(src => src.PersonaID))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.PrimerApellido,
                    opt => opt.MapFrom(src => src.PrimerApellido))
                .ForMember(dest => dest.SegundoApellido,
                    opt => opt.MapFrom(src => src.SegundoApellido))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email));

                config.CreateMap<TipoRubro, TipoRubroDto>()
                .ForMember(dest => dest.TipoRubroID,
                    opt => opt.MapFrom(src => src.TipoRubroID))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.Nombre));

                config.CreateMap<Rubro, RubroDto>()
                .ForMember(dest => dest.RubroID,
                    opt => opt.MapFrom(src => src.RubroID))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.TipoRubro,
                    opt => opt.MapFrom(src => src.TipoRubro1.Nombre))
                .ForMember(dest => dest.Descripcion,
                    opt => opt.MapFrom(src => src.Descripcion));
            });
        }
    }
}