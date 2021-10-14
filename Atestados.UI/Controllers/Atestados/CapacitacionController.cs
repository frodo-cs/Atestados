﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Atestados.Datos.Modelo;
using Atestados.Negocios.Negocios;
using Atestados.Objetos.Dtos;
using Newtonsoft.Json;

namespace Atestados.UI.Controllers.Atestados
{
    public class CapacitacionController : Controller
    {
        private AtestadosEntities db = new AtestadosEntities();
        private InformacionAtestado infoAtestado = new InformacionAtestado();
        private InformacionGeneral infoGeneral = new InformacionGeneral();
        private List<ArchivoDTO> archivos = new List<ArchivoDTO>();

        private readonly int Capacitacion = 35;

        // GET: Libros
        public ActionResult Index()
        {
            return View(infoAtestado.CargarAtestadosDeTipo(Capacitacion));
        }

        // GET: Libro/Ver
        public ActionResult Ver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtestadoDTO atestado = infoAtestado.CargarAtestado(id);
            if (atestado == null)
            {
                return HttpNotFound();
            }
            return View(atestado);
        }

        // GET: Libro/Crear
        public ActionResult Crear()
        {
            AtestadoDTO capacitacion = new AtestadoDTO();
            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre");
            return View(capacitacion);
        }

        // POST: Libro/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "Fecha,Archivos,AtestadoID,Enlace,HoraCreacion,Nombre,CantidadHoras,Observaciones,Persona,PersonaID,RubroID,FechaInicio,FechaFinal")] AtestadoDTO atestado)
        {
            if (ModelState.IsValid)
            {
                atestado.RubroID = Capacitacion;
                atestado.PaisID = 52; // Costa Rica
                Atestado a = AutoMapper.Mapper.Map<AtestadoDTO, Atestado>(atestado);
                infoAtestado.GuardarAtestado(a);
                atestado.AtestadoID = a.AtestadoID;
                Fecha fecha = AutoMapper.Mapper.Map<AtestadoDTO, Fecha>(atestado);
                infoAtestado.GuardarFecha(fecha);

                foreach (ArchivoDTO archivo in archivos)
                {
                    Archivo ar = AutoMapper.Mapper.Map<ArchivoDTO, Archivo>(archivo);
                    ar.AtestadoID = a.AtestadoID;
                    infoAtestado.GuardarArchivo(ar);
                }

                return RedirectToAction("Index");
            }

            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre", atestado.PersonaID);
            return View(atestado);
        }

        // GET: Libro/Editar
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atestado atestado = infoAtestado.CargarAtestadoParaEditar(id);
            if (atestado == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre", atestado.PersonaID);
            return View(atestado);
        }

        // POST: Libro/Editar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Fecha,Archivos,AtestadoID,Enlace,HoraCreacion,Nombre,CantidadHoras,Observaciones,Persona,PersonaID,RubroID,FechaInicio,FechaFinal")] Atestado atestado)
        {
            if (ModelState.IsValid)
            {
                infoAtestado.EditarAtestado(atestado);
                return RedirectToAction("Index");
            }

            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre", atestado.PersonaID);
            return View(atestado);
        }

        // GET: Libro/Borrar
        public ActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atestado atestado = infoAtestado.CargarAtestadoParaBorrar(id);
            if (atestado == null)
            {
                return HttpNotFound();
            }
            return View(atestado);
        }

        // POST: Libro/Borrar
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult Borrar(int id)
        {
            infoAtestado.BorrarAtestado(id);
            return RedirectToAction("Index");
        }

       
        [HttpPost]
        public JsonResult Cargar(HttpPostedFileBase archivo)
        {
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(archivo.InputStream))
            {
                bytes = br.ReadBytes(archivo.ContentLength);
            }

            var jsonTest = JsonConvert.SerializeObject(new ArchivoDTO
            {
                AtestadoID = Capacitacion,
                Nombre = Path.GetFileName(archivo.FileName),
                TipoArchivo = archivo.ContentType,
                Datos = bytes
            });

            return Json(new
            {
                archivoJson = jsonTest
            });
        }

        [HttpPost]
        public FileResult Descargar(int? archivoID)
        {
            ArchivoDTO archivo = infoAtestado.CargarArchivo(archivoID);
            return File(archivo.Datos, archivo.TipoArchivo, archivo.Nombre);
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