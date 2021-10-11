using Atestados.Datos.Modelo;
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
                config.CreateMap<Persona, PersonaDTO>()
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

                config.CreateMap<TipoRubro, TipoRubroDTO>()
                .ForMember(dest => dest.TipoRubroID,
                    opt => opt.MapFrom(src => src.TipoRubroID))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.Nombre));

                config.CreateMap<Rubro, RubroDTO>()
                .ForMember(dest => dest.RubroID,
                    opt => opt.MapFrom(src => src.RubroID))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.TipoRubro,
                    opt => opt.MapFrom(src => src.TipoRubro1.Nombre))
                .ForMember(dest => dest.Descripcion,
                    opt => opt.MapFrom(src => src.Descripcion));

                config.CreateMap<Pais, PaisDTO>()
                .ForMember(dest => dest.PaisID,
                    opt => opt.MapFrom(src => src.PaisID))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.Nombre));

                config.CreateMap<Idioma, IdiomaDTO>()
                .ForMember(dest => dest.IdiomaID,
                    opt => opt.MapFrom(src => src.IdiomaID))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.Nombre));

                config.CreateMap<Fecha, FechaDTO>()
                .ForMember(dest => dest.FechaID,
                    opt => opt.MapFrom(src => src.FechaID))
                .ForMember(dest => dest.FechaInicio,
                    opt => opt.MapFrom(src => src.FechaInicio))
                .ForMember(dest => dest.FechaFinal,
                    opt => opt.MapFrom(src => src.FechaFinal))
                .ForMember(dest => dest.Atestado,
                    opt => opt.MapFrom(src => src.Atestado.Nombre));

                config.CreateMap<InfoEditorial, InfoEditorialDTO>()
                .ForMember(dest => dest.InfoEditorialID,
                    opt => opt.MapFrom(src => src.InfoEditorialID))
                .ForMember(dest => dest.Editorial,
                    opt => opt.MapFrom(src => src.Editorial))
                .ForMember(dest => dest.Website,
                    opt => opt.MapFrom(src => src.Website))
                .ForMember(dest => dest.Atestado,
                    opt => opt.MapFrom(src => src.Atestado.Nombre));

                config.CreateMap<DominioIdioma, DominioIdiomaDTO>()
                .ForMember(dest => dest.DominioIdiomaID,
                    opt => opt.MapFrom(src => src.DominioIdiomaID))
                .ForMember(dest => dest.Idioma,
                    opt => opt.MapFrom(src => src.Idioma.Nombre))
                .ForMember(dest => dest.Auditiva,
                    opt => opt.MapFrom(src => src.Auditiva))
                .ForMember(dest => dest.Escrito,
                    opt => opt.MapFrom(src => src.Escrito))
                .ForMember(dest => dest.Oral,
                    opt => opt.MapFrom(src => src.Oral))
                .ForMember(dest => dest.Lectura,
                        opt => opt.MapFrom(src => src.Lectura))
                .ForMember(dest => dest.Atestado,
                        opt => opt.MapFrom(src => src.Atestado.Nombre));

                config.CreateMap<Archivo, ArchivoDTO>()
                .ForMember(dest => dest.ArchivoID,
                    opt => opt.MapFrom(src => src.ArchivoID))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Obligatorio,
                    opt => opt.MapFrom(src => Convert.ToBoolean(src.Obligatorio)))
                .ForMember(dest => dest.Datos,
                    opt => opt.MapFrom(src => src.Datos))
                .ForMember(dest => dest.Atestado,
                    opt => opt.MapFrom(src => src.Atestado.Nombre))
                .ForMember(dest => dest.TipoArchivo,
                    opt => opt.MapFrom(src => src.TipoArchivo));

                config.CreateMap<AtestadoXPersona, AtestadoXPersonaDTO>()
                .ForMember(dest => dest.AtestadoID,
                    opt => opt.MapFrom(src => src.AtestadoID))
                .ForMember(dest => dest.PersonaID,
                    opt => opt.MapFrom(src => src.PersonaID))
                .ForMember(dest => dest.Porcentaje,
                    opt => opt.MapFrom(src => src.Porcentaje))
                .ForMember(dest => dest.Departamento,
                    opt => opt.MapFrom(src => src.Departamento))
                .ForMember(dest => dest.Atestado,
                    opt => opt.MapFrom(src => src.Atestado.Nombre))
                .ForMember(dest => dest.Persona,
                    opt => opt.MapFrom(src => String.Format("{0} {1} {2}", src.Persona.Nombre, src.Persona.PrimerApellido, src.Persona.SegundoApellido)));


                config.CreateMap<Atestado, AtestadoDTO>()
                .ForMember(dest => dest.AtestadoID,
                    opt => opt.MapFrom(src => src.AtestadoID))
                .ForMember(dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.CantidadHoras,
                    opt => opt.MapFrom(src => src.CantidadHoras))
                .ForMember(dest => dest.CatalogoTipo,
                    opt => opt.MapFrom(src => src.CatalogoTipo))
                .ForMember(dest => dest.Descargado,
                    opt => opt.MapFrom(src => src.Descargado))
                .ForMember(dest => dest.Enlace,
                    opt => opt.MapFrom(src => src.Enlace))
                .ForMember(dest => dest.Enviado,
                    opt => opt.MapFrom(src => src.Enviado))
                .ForMember(dest => dest.NumeroAutores,
                    opt => opt.MapFrom(src => src.NumeroAutores))
                .ForMember(dest => dest.Observaciones,
                    opt => opt.MapFrom(src => src.Observaciones))
                .ForMember(dest => dest.HoraCreacion,
                    opt => opt.MapFrom(src => src.HoraCreacion))
                .ForMember(dest => dest.Lugar,
                    opt => opt.MapFrom(src => src.Lugar))
                .ForMember(dest => dest.Pais,
                    opt => opt.MapFrom(src => src.Pais))
                .ForMember(dest => dest.Rubro,
                    opt => opt.MapFrom(src => src.Rubro))
                .ForMember(dest => dest.Persona,
                    opt => opt.MapFrom(src => src.Persona))
                .ForMember(dest => dest.DominioIdioma,
                    opt => opt.MapFrom(src => src.DominioIdioma))
                .ForMember(dest => dest.Archivos,
                    opt => opt.MapFrom(src => src.Archivo))
                .ForMember(dest => dest.AtestadoXPersona,
                    opt => opt.MapFrom(src => src.AtestadoXPersona))
                .ForMember(dest => dest.Fecha,
                    opt => opt.MapFrom(src => src.Fecha))
                .ForMember(dest => dest.InfoEditorial,
                    opt => opt.MapFrom(src => src.InfoEditorial));

                config.CreateMap<Atestado, Atestado>()
               .ForMember(dest => dest.Nombre,
                   opt => opt.MapFrom(src => src.Nombre))
               .ForMember(dest => dest.Nombre,
                   opt => opt.MapFrom(src => src.Nombre))
               .ForMember(dest => dest.NumeroAutores,
                   opt => opt.MapFrom(src => src.NumeroAutores))
               .ForMember(dest => dest.HoraCreacion,
                   opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.Enlace,
                   opt => opt.MapFrom(src => src.Enlace))
               .ForMember(dest => dest.PaisID,
                   opt => opt.MapFrom(src => src.PaisID))
               .ForMember(dest => dest.Observaciones,
                   opt => opt.MapFrom(src => src.Observaciones))
               .ForMember(dest => dest.Persona,
                   opt => opt.MapFrom(src => src.Persona))
               .ForMember(dest => dest.RubroID,
                   opt => opt.MapFrom(src => 1));

                config.CreateMap<LibroDTO, Atestado>()
               .ForMember(dest => dest.Nombre,
                   opt => opt.MapFrom(src => src.Nombre))
               .ForMember(dest => dest.Nombre,
                   opt => opt.MapFrom(src => src.Nombre))
               .ForMember(dest => dest.NumeroAutores,
                   opt => opt.MapFrom(src => src.NumeroAutores))
               .ForMember(dest => dest.HoraCreacion,
                   opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.Enlace,
                   opt => opt.MapFrom(src => src.Enlace))
               .ForMember(dest => dest.PaisID,
                   opt => opt.MapFrom(src => src.PaisID))
               .ForMember(dest => dest.Observaciones,
                   opt => opt.MapFrom(src => src.Observaciones))
               .ForMember(dest => dest.Persona,
                   opt => opt.MapFrom(src => src.Persona))
               .ForMember(dest => dest.PersonaID,
                   opt => opt.MapFrom(src => src.PersonaID))
               .ForMember(dest => dest.RubroID,
                   opt => opt.MapFrom(src => 1));

                config.CreateMap<LibroDTO, InfoEditorial>()
                .ForMember(dest => dest.InfoEditorialID,
                    opt => opt.MapFrom(src => src.AtestadoID))
                .ForMember(dest => dest.Editorial,
                    opt => opt.MapFrom(src => src.Editorial))
                .ForMember(dest => dest.Website,
                    opt => opt.MapFrom(src => src.Website));

                config.CreateMap<LibroDTO, Fecha>()
                .ForMember(dest => dest.FechaID,
                    opt => opt.MapFrom(src => src.AtestadoID))
                .ForMember(dest => dest.FechaFinal,
                    opt => opt.MapFrom(src => src.Annio));


            });
        }
    }
}
