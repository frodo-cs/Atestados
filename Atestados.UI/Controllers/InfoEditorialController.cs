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
    public class InfoEditorialController : Controller
    {
        private AtestadosEntities db = new AtestadosEntities();
        private InformacionAtestado info = new InformacionAtestado();

        // GET: InfoEditorial
        public ActionResult Index()
        {
            return View(info.CargarInfoEditoriales());
        }

        // GET: InfoEditorial/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfoEditorialDTO infoEditorial = info.CargarInfoEditorial(id);
            if (infoEditorial == null)
            {
                return HttpNotFound();
            }
            return View(infoEditorial);
        }

        // GET: InfoEditorial/Create
        public ActionResult Create()
        {
            ViewBag.InfoEditorialID = new SelectList(db.Atestado, "AtestadoID", "Nombre");
            return View();
        }

        // POST: InfoEditorial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InfoEditorialID,Editorial,Website")] InfoEditorial infoEditorial)
        {
            if (ModelState.IsValid)
            {
                info.GuardarInfoEditorial(infoEditorial);
                return RedirectToAction("Index");
            }

            ViewBag.InfoEditorialID = new SelectList(db.Atestado, "AtestadoID", "Nombre", infoEditorial.InfoEditorialID);
            return View(infoEditorial);
        }

        // GET: InfoEditorial/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfoEditorial infoEditorial = info.CargarInfoEditorialParaEditar(id);
            if (infoEditorial == null)
            {
                return HttpNotFound();
            }
            ViewBag.InfoEditorialID = new SelectList(db.Atestado, "AtestadoID", "Nombre", infoEditorial.InfoEditorialID);
            return View(infoEditorial);
        }

        // POST: InfoEditorial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InfoEditorialID,Editorial,Website")] InfoEditorial infoEditorial)
        {
            if (ModelState.IsValid)
            {
                info.EditarInfoEditorial(infoEditorial);
                return RedirectToAction("Index");
            }
            ViewBag.InfoEditorialID = new SelectList(db.Atestado, "AtestadoID", "Nombre", infoEditorial.InfoEditorialID);
            return View(infoEditorial);
        }

        // GET: InfoEditorial/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfoEditorial infoEditorial = info.CargarInfoEditorialParaBorrar(id);
            if (infoEditorial == null)
            {
                return HttpNotFound();
            }
            return View(infoEditorial);
        }

        // POST: InfoEditorial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            info.BorrarInfoEditorial(id);
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
