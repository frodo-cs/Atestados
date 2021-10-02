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
    public class InformacionGeneral
    {
        private AtestadosEntities db = new AtestadosEntities();

        #region Persona
        public PersonaDTO CargarPersona(int? id)
        {
            Persona persona = db.Persona.Find(id);

            if (persona == null)
                return null;

            PersonaDTO personasDto = AutoMapper.Mapper.Map<Persona, PersonaDTO>(persona);

            return personasDto;
            
        }

        public Persona CargarPersonaParaEditar(int? id)
        {
            Persona persona = db.Persona.Find(id);

            if (persona == null)
                return null;

            return persona;

        }

        public Persona CargarPersonaParaBorrar(int? id)
        {
            Persona persona = db.Persona.Find(id);

            if (persona == null)
                return null;

            return persona;

        }

        public List<PersonaDTO> CargarPersonas()
        {

            List<Persona> listaPersona = db.Persona.ToList();

            List<PersonaDTO> listaPersonasDto = AutoMapper.Mapper.Map<List<Persona>, List<PersonaDTO>>(listaPersona);

            return listaPersonasDto;

        }

        public void GuardarPersona(Persona persona)
        {
            db.Persona.Add(persona);
            db.SaveChanges();
        }

        public void EditarPersona(Persona persona)
        {
            db.Entry(persona).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void BorrarPersona(int id)
        {
            Persona persona = db.Persona.Find(id);
            db.Persona.Remove(persona);
            db.SaveChanges();
        }
        #endregion
    }
}
