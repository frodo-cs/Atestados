using Atestados.Datos.Modelo;
using Atestados.Objetos.Dtos;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestados.Negocios.Negocios
{
    public class Catalogos
    {
        private AtestadosEntities db = new AtestadosEntities();

        #region TipoRubro
        public List<TipoRubroDto> CargarTiposDeRubros()
        {
            //var config = new MapperConfiguration(cfg => cfg.Create)

            List<TipoRubro> listaTipoRubro = db.TipoRubroes.ToList();

            List<TipoRubroDto> listaTipoRubrosDto = AutoMapper.Mapper.Map<List<TipoRubro>, List<TipoRubroDto>>(listaTipoRubro);

            return listaTipoRubrosDto;
        }

        public TipoRubroDto CargarTipoRubro(int? id)
        {
            TipoRubro rubro = db.TipoRubroes.Find(id);

            if (rubro == null)
                return null;

            TipoRubroDto rubroDto = AutoMapper.Mapper.Map<TipoRubro, TipoRubroDto>(rubro);
            return rubroDto;
        }

        public TipoRubro CargarTipoRubroParaEditar(int? id)
        {
            TipoRubro tipoRubro = db.TipoRubroes.Find(id);

            if (tipoRubro == null)
                return null;

            return tipoRubro;

        }

        public TipoRubro CargarTipoRubroParaBorrar(int? id)
        {
            TipoRubro tipoRubro = db.TipoRubroes.Find(id);

            if (tipoRubro == null)
                return null;

            return tipoRubro;

        }

        public void GuardarTipoRubro(TipoRubro tipoRubro)
        {
            db.TipoRubroes.Add(tipoRubro);
            db.SaveChanges();
        }

        public void EditarTipoRubro(TipoRubro tipoRubro)
        {
            db.Entry(tipoRubro).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void BorrarTipoRubro(int id)
        {
            TipoRubro tipoRubro = db.TipoRubroes.Find(id);
            db.TipoRubroes.Remove(tipoRubro);
            db.SaveChanges();
        }

        #endregion

        #region Rubro
        public List<RubroDto> CargarRubros()
        {
            //var config = new MapperConfiguration(cfg => cfg.Create)

            List<Rubro> listaRubro = db.Rubroes.ToList();

            List<RubroDto> listaRubrosDto = AutoMapper.Mapper.Map<List<Rubro>, List<RubroDto>>(listaRubro);

            return listaRubrosDto;
        }

        public RubroDto CargarRubro(int? id)
        {
            Rubro rubro = db.Rubroes.Find(id);

            if (rubro == null)
                return null;

            RubroDto rubroDto = AutoMapper.Mapper.Map<Rubro, RubroDto>(rubro);
            return rubroDto;
        }

        public Rubro CargarRubroParaEditar(int? id)
        {
            Rubro rubro = db.Rubroes.Find(id);

            if (rubro == null)
                return null;

            return rubro;

        }

        public Rubro CargarRubroParaBorrar(int? id)
        {
            Rubro rubro = db.Rubroes.Find(id);

            if (rubro == null)
                return null;

            return rubro;

        }

        public void GuardarRubro(Rubro rubro)
        {
            db.Rubroes.Add(rubro);
            db.SaveChanges();
        }

        public void EditarRubro(Rubro rubro)
        {
            db.Entry(rubro).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void BorrarRubro(int id)
        {
            Rubro rubro = db.Rubroes.Find(id);
            db.Rubroes.Remove(rubro);
            db.SaveChanges();
        }


        #endregion
    }
}
