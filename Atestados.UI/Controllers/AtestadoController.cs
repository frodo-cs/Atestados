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
    public class AtestadoController : Controller
    {
        private AtestadosEntities db = new AtestadosEntities();
        private InformacionAtestado info = new InformacionAtestado();

        // GET: Atestadoes
        public ActionResult Index()
        {
            return View(info.CargarAtestados());
        }

        // GET: Atestadoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtestadoDTO atestado = info.CargarAtestado(id);
            if (atestado == null)
            {
                return HttpNotFound();
            }
            return View(atestado);
        }

        // GET: Atestadoes/Create
        public ActionResult Create()
        {
            ViewBag.PaisID = new SelectList(db.Pais, "PaisID", "Nombre");
            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre");
            ViewBag.RubroID = new SelectList(db.Rubro, "RubroID", "Nombre");
            ViewBag.AtestadoID = new SelectList(db.DominioIdioma, "DominioIdiomaID", "DominioIdiomaID");
            ViewBag.AtestadoID = new SelectList(db.Fecha, "FechaID", "FechaID");
            ViewBag.AtestadoID = new SelectList(db.InfoEditorial, "InfoEditorialID", "Editorial");
            return View();
        }

        // POST: Atestadoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AtestadoID,Nombre,NumeroAutores,Observaciones,HoraCreacion,Enviado,Descargado,CantidadHoras,Lugar,CatalogoTipo,Enlace,PaisID,PersonaID,RubroID")] Atestado atestado)
        {
            if (ModelState.IsValid)
            {
                info.GuardarAtestado(atestado);
                return RedirectToAction("Index");
            }

            ViewBag.PaisID = new SelectList(db.Pais, "PaisID", "Nombre", atestado.PaisID);
            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre", atestado.PersonaID);
            ViewBag.RubroID = new SelectList(db.Rubro, "RubroID", "Nombre", atestado.RubroID);
            ViewBag.AtestadoID = new SelectList(db.DominioIdioma, "DominioIdiomaID", "DominioIdiomaID", atestado.AtestadoID);
            ViewBag.AtestadoID = new SelectList(db.Fecha, "FechaID", "FechaID", atestado.AtestadoID);
            ViewBag.AtestadoID = new SelectList(db.InfoEditorial, "InfoEditorialID", "Editorial", atestado.AtestadoID);
            return View(atestado);
        }

        // GET: Atestadoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atestado atestado = info.CargarAtestadoParaEditar(id);
            if (atestado == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaisID = new SelectList(db.Pais, "PaisID", "Nombre", atestado.PaisID);
            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre", atestado.PersonaID);
            ViewBag.RubroID = new SelectList(db.Rubro, "RubroID", "Nombre", atestado.RubroID);
            ViewBag.AtestadoID = new SelectList(db.DominioIdioma, "DominioIdiomaID", "DominioIdiomaID", atestado.AtestadoID);
            ViewBag.AtestadoID = new SelectList(db.Fecha, "FechaID", "FechaID", atestado.AtestadoID);
            ViewBag.AtestadoID = new SelectList(db.InfoEditorial, "InfoEditorialID", "Editorial", atestado.AtestadoID);
            return View(atestado);
        }

        // POST: Atestadoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AtestadoID,Nombre,NumeroAutores,Observaciones,HoraCreacion,Enviado,Descargado,CantidadHoras,Lugar,CatalogoTipo,Enlace,PaisID,PersonaID,RubroID")] Atestado atestado)
        {
            if (ModelState.IsValid)
            {
                info.EditarAtestado(atestado);
                return RedirectToAction("Index");
            }
            ViewBag.PaisID = new SelectList(db.Pais, "PaisID", "Nombre", atestado.PaisID);
            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre", atestado.PersonaID);
            ViewBag.RubroID = new SelectList(db.Rubro, "RubroID", "Nombre", atestado.RubroID);
            ViewBag.AtestadoID = new SelectList(db.DominioIdioma, "DominioIdiomaID", "DominioIdiomaID", atestado.AtestadoID);
            ViewBag.AtestadoID = new SelectList(db.Fecha, "FechaID", "FechaID", atestado.AtestadoID);
            ViewBag.AtestadoID = new SelectList(db.InfoEditorial, "InfoEditorialID", "Editorial", atestado.AtestadoID);
            return View(atestado);
        }

        // GET: Atestadoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atestado atestado = info.CargarAtestadoParaBorrar(id);
            if (atestado == null)
            {
                return HttpNotFound();
            }
            return View(atestado);
        }

        // POST: Atestadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            info.BorrarAtestado(id);
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
