using System;
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
    public class LibroController : Controller
    {
        private AtestadosEntities db = new AtestadosEntities();
        private InformacionAtestado info = new InformacionAtestado();
        private List<ArchivoDTO> archivos = new List<ArchivoDTO>();
        private List<AutorDTO> autores = new List<AutorDTO>();

        private readonly int Libro = 1;

        // GET: Libros
        public ActionResult Index()
        {
            return View(info.CargarAtestadosDeTipo(Libro));
        }

        // GET: Libro/Ver
        public ActionResult Ver(int? id)
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

        // GET: Libro/Crear
        public ActionResult Crear()
        {
            LibroDTO libro = new LibroDTO();
            libro.Archivos = archivos;
            ViewBag.PaisID = new SelectList(db.Pais, "PaisID", "Nombre");
            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre");
            ViewBag.AtestadoID = new SelectList(db.Fecha, "FechaID", "FechaID");
            ViewBag.AtestadoID = new SelectList(db.InfoEditorial, "InfoEditorialID", "Editorial");        
            return View(libro);
        }

        // POST: Libro/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "Annio,Archivos,AtestadoID,AtestadoXPersona,Editorial,Enlace,HoraCreacion,Nombre,NumeroAutores,Observaciones,PaisID,Persona,PersonaID,RubroID,Website")] LibroDTO atestado)
        {
            if (ModelState.IsValid)
            {
                Atestado a = AutoMapper.Mapper.Map<LibroDTO, Atestado>(atestado);               
                info.GuardarAtestado(a);
                atestado.AtestadoID = a.AtestadoID;
                InfoEditorial infoEditorial = AutoMapper.Mapper.Map<LibroDTO, InfoEditorial>(atestado);
                info.GuardarInfoEditorial(infoEditorial);
                Fecha fecha = AutoMapper.Mapper.Map<LibroDTO, Fecha>(atestado);
                info.GuardarFecha(fecha);
                return RedirectToAction("Index");
            }

            ViewBag.PaisID = new SelectList(db.Pais, "PaisID", "Nombre", atestado.PaisID);
            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre", atestado.PersonaID);
            ViewBag.AtestadoID = new SelectList(db.Fecha, "FechaID", "FechaID", atestado.AtestadoID);
            ViewBag.AtestadoID = new SelectList(db.InfoEditorial, "InfoEditorialID", "Editorial", atestado.AtestadoID);
            ViewBag.RubroID = Libro;
            return View(atestado);
        }

        // GET: Libro/Editar
        public ActionResult Editar(int? id)
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
            ViewBag.AtestadoID = new SelectList(db.Fecha, "FechaID", "FechaID", atestado.AtestadoID);
            ViewBag.AtestadoID = new SelectList(db.InfoEditorial, "InfoEditorialID", "Editorial", atestado.AtestadoID);
            return View(atestado);
        }

        // POST: Libro/Editar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Annio,Archivos,AtestadoID,AtestadoXPersona,Editorial,Enlace,HoraCreacion,Nombre,NumeroAutores,Observaciones,PaisID,Persona,PersonaID,RubroID,Website")] Atestado atestado)
        {
            if (ModelState.IsValid)
            {
                info.EditarAtestado(atestado);
                return RedirectToAction("Index");
            }
            ViewBag.PaisID = new SelectList(db.Pais, "PaisID", "Nombre", atestado.PaisID);
            ViewBag.PersonaID = new SelectList(db.Persona, "PersonaID", "Nombre", atestado.PersonaID);
            ViewBag.RubroID = new SelectList(db.Rubro, "RubroID", "Nombre", atestado.RubroID);
            ViewBag.AtestadoID = new SelectList(db.Fecha, "FechaID", "FechaID", atestado.AtestadoID);
            ViewBag.AtestadoID = new SelectList(db.InfoEditorial, "InfoEditorialID", "Editorial", atestado.AtestadoID);
            return View(atestado);
        }

        // GET: Libro/Borrar
        public ActionResult Borrar(int? id)
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

        // POST: Libro/Borrar
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult Borrar(int id)
        {
            info.BorrarAtestado(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult AgregarAutor(AutorDTO autorData)
        {

            AutorDTO autor = new AutorDTO()
            {
                Nombre = autorData.Nombre,
                PrimerApellido = autorData.PrimerApellido,
                SegundoApellido = autorData.SegundoApellido,
                Porcentaje = autorData.Porcentaje,
                Email = autorData.Email
            };

            autores.Add(autor);

            var jsonTest = JsonConvert.SerializeObject(autor);

            return Json(new
            {
                personaJson = jsonTest
            });
        }

        [HttpPost]
        public JsonResult Cargar(HttpPostedFileBase archivo)
        {
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(archivo.InputStream))
            {
                bytes = br.ReadBytes(archivo.ContentLength);
            }

            archivos.Add(new ArchivoDTO
            {
                AtestadoID = Libro,
                Nombre = Path.GetFileName(archivo.FileName),
                TipoArchivo = archivo.ContentType,
                Datos = bytes
            });

            var jsonTest = JsonConvert.SerializeObject(archivos);

            return Json(new {
                archivosJson = jsonTest
            });
        }

        [HttpPost]
        public FileResult Descargar(int? archivoID)
        {
            ArchivoDTO archivo = info.CargarArchivo(archivoID);
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