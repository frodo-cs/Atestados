using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atestados.Datos.Modelo;
using Atestados.Objetos;
using Atestados.Objetos.Dtos;
using AutoMapper;

namespace Atestados.Negocios.Negocios
{
    public class InformaciónGeneral
    {
        private AtestadosEntities db = new AtestadosEntities();

        
        public PersonasDto CargarPersona(int? id)
        {
            Persona persona = db.Personas.Find(id);

            if (persona == null)
                return null;

            PersonasDto personasDto = AutoMapper.Mapper.Map<Persona, PersonasDto>(persona);

            return personasDto;
            
        }

        public Persona CargarPersonaParaEditar(int? id)
        {
            Persona persona = db.Personas.Find(id);

            if (persona == null)
                return null;

            return persona;

        }

        public Persona CargarPersonaParaBorrar(int? id)
        {
            Persona persona = db.Personas.Find(id);

            if (persona == null)
                return null;

            return persona;

        }

        public List<PersonasDto> CargarPersonas()
        {
            //var config = new MapperConfiguration(cfg => cfg.Create)

            List<Persona> listaPersona = db.Personas.ToList();

            List<PersonasDto> listaPersonasDto = AutoMapper.Mapper.Map<List<Persona>, List<PersonasDto>>(listaPersona);

            return listaPersonasDto;

        }

        public void GuardarPersona(Persona persona)
        {
            db.Personas.Add(persona);
            db.SaveChanges();
        }

        public void EditarPersona(Persona persona)
        {
            db.Entry(persona).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void BorrarPersona(int id)
        {
            Persona persona = db.Personas.Find(id);
            db.Personas.Remove(persona);
            db.SaveChanges();
        }
    }
}
