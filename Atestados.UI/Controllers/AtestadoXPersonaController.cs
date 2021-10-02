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
    public class AtestadoXPersonaController : Controller
    {
        private AtestadosEntities db = new AtestadosEntities();
        private InformacionAtestado info = new InformacionAtestado();

        // GET: AtestadoXPersona
        public ActionResult Index()
        {
            return View(info.CargarAtestadoXPersonas());
        }

        // GET: AtestadoXPersona/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtestadoXPersonaDTO atestadoXPersona = info.CargarAtestadoXPersona(id);
            if (atestadoXPersona == null)
            {
                return HttpNotFound();
            }
            return View(atestadoXPersona);
        }

        // GET: AtestadoXPersona/Create
        public ActionResult Create()
        {
            ViewBag.AtestadoID = new SelectList(db.Atestado, "AtestadoID", "Nombre");
            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre");
            return View();
        }

        // POST: AtestadoXPersona/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AtestadoID,PersonaID,Porcentaje,Departamento")] AtestadoXPersona atestadoXPersona)
        {
            if (ModelState.IsValid)
            {
                info.GuardarAtestadoXPersona(atestadoXPersona);
                return RedirectToAction("Index");
            }

            ViewBag.AtestadoID = new SelectList(db.Atestado, "AtestadoID", "Nombre", atestadoXPersona.AtestadoID);
            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre", atestadoXPersona.PersonaID);
            return View(atestadoXPersona);
        }

        // GET: AtestadoXPersona/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtestadoXPersona atestadoXPersona = info.CargarAtestadoXPersonaParaEditar(id);
            if (atestadoXPersona == null)
            {
                return HttpNotFound();
            }
            ViewBag.AtestadoID = new SelectList(db.Atestado, "AtestadoID", "Nombre", atestadoXPersona.AtestadoID);
            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre", atestadoXPersona.PersonaID);
            return View(atestadoXPersona);
        }

        // POST: AtestadoXPersona/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AtestadoID,PersonaID,Porcentaje,Departamento")] AtestadoXPersona atestadoXPersona)
        {
            if (ModelState.IsValid)
            {
                info.EditarAtestadoXPersona(atestadoXPersona);
                return RedirectToAction("Index");
            }
            ViewBag.AtestadoID = new SelectList(db.Atestado, "AtestadoID", "Nombre", atestadoXPersona.AtestadoID);
            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre", atestadoXPersona.PersonaID);
            return View(atestadoXPersona);
        }

        // GET: AtestadoXPersona/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtestadoXPersona atestadoXPersona = info.CargarAtestadoXPersonaParaBorrar(id);
            if (atestadoXPersona == null)
            {
                return HttpNotFound();
            }
            return View(atestadoXPersona);
        }

        // POST: AtestadoXPersona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            info.BorrarAtestadoXPersona(id);
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
