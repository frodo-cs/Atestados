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
    public class ArchivoController : Controller
    {
        private AtestadosEntities db = new AtestadosEntities();
        private InformacionAtestado info = new InformacionAtestado();

        // GET: Archivo
        public ActionResult Index()
        {
            return View(info.CargarArchivos());
        }

        // GET: Archivo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArchivoDTO archivo = info.CargarArchivo(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            return View(archivo);
        }

        // GET: Archivo/Create
        public ActionResult Create()
        {
            ViewBag.AtestadoID = new SelectList(db.Atestado, "AtestadoID", "Nombre");
            return View();
        }

        // POST: Archivo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArchivoID,Obligatorio,Nombre,Datos,TipoArchivo,AtestadoID")] Archivo archivo)
        {
            if (ModelState.IsValid)
            {
                info.GuardarArchivo(archivo);
                return RedirectToAction("Index");
            }

            ViewBag.AtestadoID = new SelectList(db.Atestado, "AtestadoID", "Nombre", archivo.AtestadoID);
            return View(archivo);
        }

        // GET: Archivo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivo archivo = info.CargarArchivoParaEditar(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.AtestadoID = new SelectList(db.Atestado, "AtestadoID", "Nombre", archivo.AtestadoID);
            return View(archivo);
        }

        // POST: Archivo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArchivoID,Obligatorio,Nombre,Datos,TipoArchivo,AtestadoID")] Archivo archivo)
        {
            if (ModelState.IsValid)
            {
                info.EditarArchivo(archivo);
                return RedirectToAction("Index");
            }
            ViewBag.AtestadoID = new SelectList(db.Atestado, "AtestadoID", "Nombre", archivo.AtestadoID);
            return View(archivo);
        }

        // GET: Archivo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivo archivo = info.CargarArchivoParaBorrar(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            return View(archivo);
        }

        // POST: Archivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            info.BorrarArchivo(id);
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
