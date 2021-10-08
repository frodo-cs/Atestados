using Atestados.Datos.Modelo;
using Atestados.Objetos.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestados.Negocios.Negocios
{
    public class InformacionAtestado
    {
        private AtestadosEntities db = new AtestadosEntities();

        #region Atestado

        public AtestadoDTO CargarAtestado(int? id)
        {
            Atestado atestado = db.Atestado.Find(id);

            if (atestado == null)
                return null;

            AtestadoDTO atestadosDto = AutoMapper.Mapper.Map<Atestado, AtestadoDTO>(atestado);

            return atestadosDto;

        }

        public Atestado CargarAtestadoParaEditar(int? id)
        {
            Atestado atestado = db.Atestado.Find(id);

            if (atestado == null)
                return null;

            return atestado;

        }

        public Atestado CargarAtestadoParaBorrar(int? id)
        {
            Atestado atestado = db.Atestado.Find(id);

            if (atestado == null)
                return null;

            return atestado;

        }

        public List<AtestadoDTO> CargarAtestados()
        {

            List<Atestado> listaAtestado = db.Atestado.Include(a => a.Pais).Include(a => a.Persona).Include(a => a.Rubro).Include(a => a.DominioIdioma).Include(a => a.Fecha).Include(a => a.InfoEditorial).ToList();

            List<AtestadoDTO> listaAtestadosDto = AutoMapper.Mapper.Map<List<Atestado>, List<AtestadoDTO>>(listaAtestado);

            return listaAtestadosDto;

        }

        public List<AtestadoDTO> CargarAtestadosDeTipo(int? rubroId)
        {

            List<Atestado> listaAtestado = db.Atestado.Where(l => l.RubroID == rubroId).ToList();

            List<AtestadoDTO> listaAtestadosDto = AutoMapper.Mapper.Map<List<Atestado>, List<AtestadoDTO>>(listaAtestado);

            return listaAtestadosDto;

        }

        public void GuardarAtestado(Atestado atestado)
        {
            db.Atestado.Add(atestado);
            db.SaveChanges();
        }

        public void EditarAtestado(Atestado atestado)
        {
            db.Entry(atestado).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void BorrarAtestado(int id)
        {
            Atestado atestado = db.Atestado.Find(id);
            db.Atestado.Remove(atestado);
            db.SaveChanges();
        }

        #endregion

        #region InfoEditorial
        public InfoEditorialDTO CargarInfoEditorial(int? id)
        {
            InfoEditorial infoEditorial = db.InfoEditorial.Find(id);

            if (infoEditorial == null)
                return null;

            InfoEditorialDTO infoEditorialsDto = AutoMapper.Mapper.Map<InfoEditorial, InfoEditorialDTO>(infoEditorial);

            return infoEditorialsDto;

        }

        public InfoEditorial CargarInfoEditorialParaEditar(int? id)
        {
            InfoEditorial infoEditorial = db.InfoEditorial.Find(id);

            if (infoEditorial == null)
                return null;

            return infoEditorial;

        }

        public InfoEditorial CargarInfoEditorialParaBorrar(int? id)
        {
            InfoEditorial infoEditorial = db.InfoEditorial.Find(id);

            if (infoEditorial == null)
                return null;

            return infoEditorial;

        }

        public List<InfoEditorialDTO> CargarInfoEditoriales()
        {

            List<InfoEditorial> listaInfoEditorial = db.InfoEditorial.Include(i => i.Atestado).ToList();

            List<InfoEditorialDTO> listaInfoEditorialsDto = AutoMapper.Mapper.Map<List<InfoEditorial>, List<InfoEditorialDTO>>(listaInfoEditorial);

            return listaInfoEditorialsDto;

        }

        public void GuardarInfoEditorial(InfoEditorial infoEditorial)
        {
            db.InfoEditorial.Add(infoEditorial);
            db.SaveChanges();
        }

        public void EditarInfoEditorial(InfoEditorial infoEditorial)
        {
            db.Entry(infoEditorial).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void BorrarInfoEditorial(int id)
        {
            InfoEditorial infoEditorial = db.InfoEditorial.Find(id);
            db.InfoEditorial.Remove(infoEditorial);
            db.SaveChanges();
        }
        #endregion

        #region Fecha
        public FechaDTO CargarFecha(int? id)
        {
            Fecha fecha = db.Fecha.Find(id);

            if (fecha == null)
                return null;

            FechaDTO fechasDto = AutoMapper.Mapper.Map<Fecha, FechaDTO>(fecha);

            return fechasDto;

        }

        public Fecha CargarFechaParaEditar(int? id)
        {
            Fecha fecha = db.Fecha.Find(id);

            if (fecha == null)
                return null;

            return fecha;

        }

        public Fecha CargarFechaParaBorrar(int? id)
        {
            Fecha fecha = db.Fecha.Find(id);

            if (fecha == null)
                return null;

            return fecha;

        }

        public List<FechaDTO> CargarFechas()
        {

            List<Fecha> listaFecha = db.Fecha.Include(f => f.Atestado).ToList();

            List<FechaDTO> listaFechasDto = AutoMapper.Mapper.Map<List<Fecha>, List<FechaDTO>>(listaFecha);

            return listaFechasDto;

        }

        public void GuardarFecha(Fecha fecha)
        {
            db.Fecha.Add(fecha);
            db.SaveChanges();
        }

        public void EditarFecha(Fecha fecha)
        {
            db.Entry(fecha).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void BorrarFecha(int id)
        {
            Fecha fecha = db.Fecha.Find(id);
            db.Fecha.Remove(fecha);
            db.SaveChanges();
        }
        #endregion

        #region Archivo
        public ArchivoDTO CargarArchivo(int? id)
        {
            Archivo archivo = db.Archivo.Find(id);

            if (archivo == null)
                return null;

            ArchivoDTO archivosDto = AutoMapper.Mapper.Map<Archivo, ArchivoDTO>(archivo);

            return archivosDto;

        }

        public Archivo CargarArchivoParaEditar(int? id)
        {
            Archivo archivo = db.Archivo.Find(id);

            if (archivo == null)
                return null;

            return archivo;

        }

        public Archivo CargarArchivoParaBorrar(int? id)
        {
            Archivo archivo = db.Archivo.Find(id);

            if (archivo == null)
                return null;

            return archivo;

        }

        public List<ArchivoDTO> CargarArchivos()
        {

            List<Archivo> listaArchivo = db.Archivo.ToList();

            List<ArchivoDTO> listaArchivosDto = AutoMapper.Mapper.Map<List<Archivo>, List<ArchivoDTO>>(listaArchivo);

            return listaArchivosDto;

        }

        public void GuardarArchivo(Archivo archivo)
        {
            db.Archivo.Add(archivo);
            db.SaveChanges();
        }

        public void EditarArchivo(Archivo archivo)
        {
            db.Entry(archivo).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void BorrarArchivo(int id)
        {
            Archivo archivo = db.Archivo.Find(id);
            db.Archivo.Remove(archivo);
            db.SaveChanges();
        }
        #endregion

        #region AtestadoXPersona

        public AtestadoXPersonaDTO CargarAtestadoXPersona(int? id)
        {
            AtestadoXPersona atestadoXPersona = db.AtestadoXPersona.Find(id);

            if (atestadoXPersona == null)
                return null;

            AtestadoXPersonaDTO atestadoXPersonasDto = AutoMapper.Mapper.Map<AtestadoXPersona, AtestadoXPersonaDTO>(atestadoXPersona);

            return atestadoXPersonasDto;

        }

        public AtestadoXPersona CargarAtestadoXPersonaParaEditar(int? id)
        {
            AtestadoXPersona atestadoXPersona = db.AtestadoXPersona.Find(id);

            if (atestadoXPersona == null)
                return null;

            return atestadoXPersona;

        }

        public AtestadoXPersona CargarAtestadoXPersonaParaBorrar(int? id)
        {
            AtestadoXPersona atestadoXPersona = db.AtestadoXPersona.Find(id);

            if (atestadoXPersona == null)
                return null;

            return atestadoXPersona;

        }

        public List<AtestadoXPersonaDTO> CargarAtestadoXPersonas()
        {

            List<AtestadoXPersona> listaAtestadoXPersona = db.AtestadoXPersona.Include(a => a.Atestado).Include(a => a.Persona).ToList();

            List<AtestadoXPersonaDTO> listaAtestadoXPersonasDto = AutoMapper.Mapper.Map<List<AtestadoXPersona>, List<AtestadoXPersonaDTO>>(listaAtestadoXPersona);

            return listaAtestadoXPersonasDto;

        }

        public void GuardarAtestadoXPersona(AtestadoXPersona atestadoXPersona)
        {
            db.AtestadoXPersona.Add(atestadoXPersona);
            db.SaveChanges();
        }

        public void EditarAtestadoXPersona(AtestadoXPersona atestadoXPersona)
        {
            db.Entry(atestadoXPersona).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void BorrarAtestadoXPersona(int id)
        {
            AtestadoXPersona atestadoXPersona = db.AtestadoXPersona.Find(id);
            db.AtestadoXPersona.Remove(atestadoXPersona);
            db.SaveChanges();
        }

        #endregion

        #region DominioIdioma

        public DominioIdiomaDTO CargarDominioIdioma(int? id)
        {
            DominioIdioma dominioIdioma = db.DominioIdioma.Find(id);

            if (dominioIdioma == null)
                return null;

            DominioIdiomaDTO dominioIdiomasDto = AutoMapper.Mapper.Map<DominioIdioma, DominioIdiomaDTO>(dominioIdioma);

            return dominioIdiomasDto;

        }

        public DominioIdioma CargarDominioIdiomaParaEditar(int? id)
        {
            DominioIdioma dominioIdioma = db.DominioIdioma.Find(id);

            if (dominioIdioma == null)
                return null;

            return dominioIdioma;

        }

        public DominioIdioma CargarDominioIdiomaParaBorrar(int? id)
        {
            DominioIdioma dominioIdioma = db.DominioIdioma.Find(id);

            if (dominioIdioma == null)
                return null;

            return dominioIdioma;

        }

        public List<DominioIdiomaDTO> CargarDominioIdiomas()
        {

            List<DominioIdioma> listaDominioIdioma = db.DominioIdioma.Include(a => a.Atestado).Include(a => a.Idioma).ToList();

            List<DominioIdiomaDTO> listaDominioIdiomasDto = AutoMapper.Mapper.Map<List<DominioIdioma>, List<DominioIdiomaDTO>>(listaDominioIdioma);

            return listaDominioIdiomasDto;

        }

        public void GuardarDominioIdioma(DominioIdioma dominioIdioma)
        {
            db.DominioIdioma.Add(dominioIdioma);
            db.SaveChanges();
        }

        public void EditarDominioIdioma(DominioIdioma dominioIdioma)
        {
            db.Entry(dominioIdioma).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void BorrarDominioIdioma(int id)
        {
            DominioIdioma dominioIdioma = db.DominioIdioma.Find(id);
            db.DominioIdioma.Remove(dominioIdioma);
            db.SaveChanges();
        }

        #endregion
    }
}
