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
    public class FechaController : Controller
    {
        private AtestadosEntities db = new AtestadosEntities();
        private InformacionAtestado info = new InformacionAtestado();

        // GET: Fecha
        public ActionResult Index()
        {
            return View(info.CargarFechas());
        }

        // GET: Fecha/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FechaDTO fecha = info.CargarFecha(id);
            if (fecha == null)
            {
                return HttpNotFound();
            }
            return View(fecha);
        }

        // GET: Fecha/Create
        public ActionResult Create()
        {
            ViewBag.FechaID = new SelectList(db.Atestado, "AtestadoID", "Nombre");
            return View();
        }

        // POST: Fecha/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FechaID,FechaInicio,FechaFinal")] Fecha fecha)
        {
            if (ModelState.IsValid)
            {
                info.GuardarFecha(fecha);
                return RedirectToAction("Index");
            }

            ViewBag.FechaID = new SelectList(db.Atestado, "AtestadoID", "Nombre", fecha.FechaID);
            return View(fecha);
        }

        // GET: Fecha/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fecha fecha = info.CargarFechaParaEditar(id);
            if (fecha == null)
            {
                return HttpNotFound();
            }
            ViewBag.FechaID = new SelectList(db.Atestado, "AtestadoID", "Nombre", fecha.FechaID);
            return View(fecha);
        }

        // POST: Fecha/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FechaID,FechaInicio,FechaFinal")] Fecha fecha)
        {
            if (ModelState.IsValid)
            {
                info.EditarFecha(fecha);
                return RedirectToAction("Index");
            }
            ViewBag.FechaID = new SelectList(db.Atestado, "AtestadoID", "Nombre", fecha.FechaID);
            return View(fecha);
        }

        // GET: Fecha/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fecha fecha = info.CargarFechaParaBorrar(id);
            if (fecha == null)
            {
                return HttpNotFound();
            }
            return View(fecha);
        }

        // POST: Fecha/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            info.BorrarFecha(id);
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
