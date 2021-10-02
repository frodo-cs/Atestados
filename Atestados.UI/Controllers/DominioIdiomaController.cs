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
    public class DominioIdiomaController : Controller
    {
        private AtestadosEntities db = new AtestadosEntities();
        private InformacionAtestado info = new InformacionAtestado();

        // GET: DominioIdioma
        public ActionResult Index()
        {
            return View(info.CargarDominioIdiomas());
        }

        // GET: DominioIdioma/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DominioIdiomaDTO dominioIdioma = info.CargarDominioIdioma(id);
            if (dominioIdioma == null)
            {
                return HttpNotFound();
            }
            return View(dominioIdioma);
        }

        // GET: DominioIdioma/Create
        public ActionResult Create()
        {
            ViewBag.DominioIdiomaID = new SelectList(db.Atestado, "AtestadoID", "Nombre");
            ViewBag.IdiomaID = new SelectList(db.Idioma, "IdiomaID", "Nombre");
            return View();
        }

        // POST: DominioIdioma/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DominioIdiomaID,IdiomaID,Lectura,Escrito,Auditiva,Oral")] DominioIdioma dominioIdioma)
        {
            if (ModelState.IsValid)
            {
                info.GuardarDominioIdioma(dominioIdioma);
                return RedirectToAction("Index");
            }

            ViewBag.DominioIdiomaID = new SelectList(db.Atestado, "AtestadoID", "Nombre", dominioIdioma.DominioIdiomaID);
            ViewBag.IdiomaID = new SelectList(db.Idioma, "IdiomaID", "Nombre", dominioIdioma.IdiomaID);
            return View(dominioIdioma);
        }

        // GET: DominioIdioma/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DominioIdioma dominioIdioma = info.CargarDominioIdiomaParaEditar(id);
            if (dominioIdioma == null)
            {
                return HttpNotFound();
            }
            ViewBag.DominioIdiomaID = new SelectList(db.Atestado, "AtestadoID", "Nombre", dominioIdioma.DominioIdiomaID);
            ViewBag.IdiomaID = new SelectList(db.Idioma, "IdiomaID", "Nombre", dominioIdioma.IdiomaID);
            return View(dominioIdioma);
        }

        // POST: DominioIdioma/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DominioIdiomaID,IdiomaID,Lectura,Escrito,Auditiva,Oral")] DominioIdioma dominioIdioma)
        {
            if (ModelState.IsValid)
            {
                info.EditarDominioIdioma(dominioIdioma);
                return RedirectToAction("Index");
            }
            ViewBag.DominioIdiomaID = new SelectList(db.Atestado, "AtestadoID", "Nombre", dominioIdioma.DominioIdiomaID);
            ViewBag.IdiomaID = new SelectList(db.Idioma, "IdiomaID", "Nombre", dominioIdioma.IdiomaID);
            return View(dominioIdioma);
        }

        // GET: DominioIdioma/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DominioIdioma dominioIdioma = info.CargarDominioIdiomaParaBorrar(id);
            if (dominioIdioma == null)
            {
                return HttpNotFound();
            }
            return View(dominioIdioma);
        }

        // POST: DominioIdioma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            info.BorrarDominioIdioma(id);
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
