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
using Atestados.Objetos.Dtos;

namespace Atestados.UI.Controllers
{
    public class RubroController : Controller
    {
        private AtestadosEntities db = new AtestadosEntities();
        Catalogos catalogos = new Catalogos();

        // GET: Rubroes
        public ActionResult Index()
        {
            return View(catalogos.CargarRubros());
        }

        // GET: Rubroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RubroDTO rubro = catalogos.CargarRubro(id);
            if (rubro == null)
            {
                return HttpNotFound();
            }
            return View(rubro);
        }

        // GET: Rubroes/Create
        public ActionResult Create()
        {
            ViewBag.TipoRubro = new SelectList(db.TipoRubro, "TipoRubroID", "Nombre");
            return View();
        }

        // POST: Rubroes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RubroID,Nombre,Descripcion,TipoRubro")] Rubro rubro)
        {
            if (ModelState.IsValid)
            {
                catalogos.GuardarRubro(rubro);
                return RedirectToAction("Index");
            }

            ViewBag.TipoRubro = new SelectList(db.TipoRubro, "TipoRubroID", "Nombre", rubro.TipoRubro);
            return View(rubro);
        }

        // GET: Rubroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rubro rubro = catalogos.CargarRubroParaEditar(id);
            if (rubro == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoRubro = new SelectList(db.TipoRubro, "TipoRubroID", "Nombre", rubro.TipoRubro);
            return View(rubro);
        }

        // POST: Rubroes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RubroID,Nombre,Descripcion,TipoRubro")] Rubro rubro)
        {
            if (ModelState.IsValid)
            {
                catalogos.EditarRubro(rubro);
                return RedirectToAction("Index");
            }
            ViewBag.TipoRubro = new SelectList(db.TipoRubro, "TipoRubroID", "Nombre", rubro.TipoRubro);
            return View(rubro);
        }

        // GET: Rubroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rubro rubro = catalogos.CargarRubroParaBorrar(id);
            if (rubro == null)
            {
                return HttpNotFound();
            }
            return View(rubro);
        }

        // POST: Rubroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            catalogos.BorrarRubro(id);
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
