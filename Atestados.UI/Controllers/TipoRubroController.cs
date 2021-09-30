using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Atestados.Datos.Modelo;
using Atestados.Negocios.Negocios;

namespace Atestados.UI.Controllers
{
    public class TipoRubroController : Controller
    {
        private AtestadosEntities db = new AtestadosEntities();
        Catalogos catalogos = new Catalogos();

        // GET: TipoRubroes
        public ActionResult Index()
        {
            return View(catalogos.CargarTiposDeRubros());
        }

        // GET: TipoRubroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipoRubro = catalogos.CargarTipoRubro(id);
            if (tipoRubro == null)
            {
                return HttpNotFound();
            }
            return View(tipoRubro);
        }

        // GET: TipoRubroes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoRubroes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoRubroID,Nombre")] TipoRubro tipoRubro)
        {
            if (ModelState.IsValid)
            {
                catalogos.GuardarTipoRubro(tipoRubro);
                return RedirectToAction("Index");
            }

            return View(tipoRubro);
        }

        // GET: TipoRubroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRubro tipoRubro = catalogos.CargarTipoRubroParaEditar(id);
            if (tipoRubro == null)
            {
                return HttpNotFound();
            }
            return View(tipoRubro);
        }

        // POST: TipoRubroes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoRubroID,Nombre")] TipoRubro tipoRubro)
        {
            if (ModelState.IsValid)
            {
                catalogos.EditarTipoRubro(tipoRubro);
                return RedirectToAction("Index");
            }
            return View(tipoRubro);
        }

        // GET: TipoRubroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRubro tipoRubro = catalogos.CargarTipoRubroParaBorrar(id);
            if (tipoRubro == null)
            {
                return HttpNotFound();
            }
            return View(tipoRubro);
        }

        // POST: TipoRubroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            catalogos.BorrarTipoRubro(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
