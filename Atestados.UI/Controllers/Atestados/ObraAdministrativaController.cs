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
    public class ObraAdministrativaController : Controller
    {
        private AtestadosEntities db = new AtestadosEntities();
        private InformacionAtestado infoAtestado = new InformacionAtestado();
        private InformacionGeneral infoGeneral = new InformacionGeneral();

        private string Rubro = "Obra administrativa de desarrollo"; // esta como otras obras profesionales

        // GET: Ponencia
        public ActionResult Index()
        {
            return View(infoAtestado.CargarAtestadosDeTipo(infoAtestado.ObtenerIDdeRubro(Rubro)));
        }

        // GET: Ponencia/Details/5
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
            ViewBag.Autores = infoAtestado.CargarAutoresAtestado(id);
            return View(atestado);
        }

        // GET: Ponencia/Create
        public ActionResult Crear()
        {
            AtestadoDTO atestado = new AtestadoDTO();
            atestado.FechaFinal = DateTime.Now;
            atestado.FechaInicio = DateTime.Now;
            atestado.NumeroAutores = 1;

            ViewBag.PaisID = new SelectList(db.Pais, "PaisID", "Nombre", infoAtestado.ObtenerIDdePais("costa rica"));
            ViewBag.Atestados = infoAtestado.CargarAtestadosDePersonaPorTipo(infoAtestado.ObtenerIDdeRubro(Rubro), (int)Session["UsuarioID"]);

            if (Session["Autores"] == null)
            {
                Session["Autores"] = new List<AutorDTO>();
            }

            if (Session["Archivos"] == null)
            {
                Session["Archivos"] = new List<ArchivoDTO>();
            }

            return View(atestado);
        }

        // POST: Ponencia/Creaar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "AtestadoID,Nombre,NumeroAutores,Observaciones,HoraCreacion,Enviado,FechaFinal,Descargado,CantidadHoras,Lugar,CatalogoTipo,Enlace,PaisID,PersonaID,RubroID")] AtestadoDTO atestado)
        {
            if (ModelState.IsValid)
            {
                atestado.PersonaID = (int)Session["UsuarioID"]; // cambiar por sesion
                atestado.RubroID = infoAtestado.ObtenerIDdeRubro(Rubro);
                atestado.PaisID = infoAtestado.ObtenerIDdePais("costa rica"); // Costa Rica
                Atestado a = AutoMapper.Mapper.Map<AtestadoDTO, Atestado>(atestado);
                infoAtestado.GuardarAtestado(a);
                atestado.AtestadoID = a.AtestadoID;
                Fecha fecha = AutoMapper.Mapper.Map<AtestadoDTO, Fecha>(atestado);
                infoAtestado.GuardarFecha(fecha);

                List<ArchivoDTO> archivos = (List<ArchivoDTO>)Session["Archivos"];
                foreach (ArchivoDTO archivo in archivos)
                {
                    Archivo ar = AutoMapper.Mapper.Map<ArchivoDTO, Archivo>(archivo);
                    ar.AtestadoID = a.AtestadoID;
                    infoAtestado.GuardarArchivo(ar);
                }
                List<AutorDTO> autores = (List<AutorDTO>)Session["Autores"];
                foreach (AutorDTO autor in autores)
                {
                    Persona persona = AutoMapper.Mapper.Map<AutorDTO, Persona>(autor);
                    infoGeneral.GuardarPersona(persona);
                    infoAtestado.GuardarAtestadoXPersona(new AtestadoXPersona()
                    {
                        AtestadoID = a.AtestadoID,
                        PersonaID = persona.PersonaID,
                        Porcentaje = autor.Porcentaje
                    });
                }

                Session["Archivos"] = new List<ArchivoDTO>();
                Session["Autores"] = new List<AutorDTO>();

                return RedirectToAction("Crear");
            }
            ViewBag.Atestados = infoAtestado.CargarAtestadosDePersonaPorTipo(infoAtestado.ObtenerIDdeRubro(Rubro), (int)Session["UsuarioID"]);
            ViewBag.PaisID = new SelectList(db.Pais, "PaisID", "Nombre", atestado.PaisID);
            return View(atestado);
        }

        // GET: Ponencia/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["Autores"] == null)
            {
                Session["Autores"] = new List<AutorDTO>();
            }

            if (Session["Archivos"] == null)
            {
                Session["Archivos"] = new List<ArchivoDTO>();
            }
            Atestado atestado = infoAtestado.CargarAtestadoParaEditar(id);
            AtestadoDTO a = AutoMapper.Mapper.Map<Atestado, AtestadoDTO>(atestado);
            if (atestado == null)
            {
                return HttpNotFound();
            }
            ViewBag.Autores = infoAtestado.CargarAutoresAtestado(atestado.AtestadoID);
            ViewBag.Atestados = infoAtestado.CargarAtestadosDePersonaPorTipo(infoAtestado.ObtenerIDdeRubro(Rubro), (int)Session["UsuarioID"]);
            ViewBag.PaisID = new SelectList(db.Pais, "PaisID", "Nombre", atestado.PaisID);
            return View(a);
        }

        // POST: Ponencia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Lugar,CantidadHoras,Archivos,CatalogoTipo,Archivos,AtestadoID,AtestadoXPersona,Editorial,Enlace,HoraCreacion,Nombre,NumeroAutores,Observaciones,PaisID,Persona,PersonaID,RubroID,Website,Fecha,DominioIdioma,Persona,Rubro,Pais,InfoEditorial,Archivo")] AtestadoDTO atestado)
        {
            if (ModelState.IsValid)
            {
                atestado.PersonaID = (int)Session["UsuarioID"]; // cambiar por sesion
                atestado.RubroID = infoAtestado.ObtenerIDdeRubro(Rubro);
                atestado.PaisID = infoAtestado.ObtenerIDdePais("costa rica"); // Costa Rica
                atestado.Fecha.FechaID = atestado.AtestadoID;
                atestado.Fecha.FechaInicio = DateTime.Now;
                infoAtestado.EditarFecha(AutoMapper.Mapper.Map<FechaDTO, Fecha>(atestado.Fecha));
                atestado.HoraCreacion = DateTime.Now;
                atestado.Archivos = infoAtestado.CargarArchivosDeAtestado(atestado.AtestadoID);
                atestado.AtestadoXPersona = AutoMapper.Mapper.Map<List<AtestadoXPersona>, List<AtestadoXPersonaDTO>>(infoAtestado.CargarAtestadoXPersonasdeAtestado(atestado.AtestadoID));
                infoAtestado.EditarAtestado(AutoMapper.Mapper.Map<AtestadoDTO, Atestado>(atestado));

                List<ArchivoDTO> archivos = (List<ArchivoDTO>)Session["Archivos"];
                foreach (ArchivoDTO archivo in archivos)
                {
                    Archivo ar = AutoMapper.Mapper.Map<ArchivoDTO, Archivo>(archivo);
                    ar.AtestadoID = atestado.AtestadoID;
                    infoAtestado.GuardarArchivo(ar);
                }
                List<AutorDTO> autores = (List<AutorDTO>)Session["Autores"];
                foreach (AutorDTO autor in autores)
                {
                    Persona persona = AutoMapper.Mapper.Map<AutorDTO, Persona>(autor);
                    infoGeneral.GuardarPersona(persona);
                    infoAtestado.GuardarAtestadoXPersona(new AtestadoXPersona()
                    {
                        AtestadoID = atestado.AtestadoID,
                        PersonaID = persona.PersonaID,
                        Porcentaje = autor.Porcentaje
                    });
                }

                Session["Archivos"] = new List<ArchivoDTO>();
                Session["Autores"] = new List<AutorDTO>();

                return RedirectToAction("Crear");
            }

            ViewBag.PaisID = new SelectList(db.Pais, "PaisID", "Nombre", atestado.PaisID);
            ViewBag.AtestadoID = new SelectList(db.Fecha, "FechaID", "FechaID", atestado.AtestadoID);
            ViewBag.AtestadoID = new SelectList(db.InfoEditorial, "InfoEditorialID", "Editorial", atestado.AtestadoID);
            ViewBag.Atestados = infoAtestado.CargarAtestadosDePersonaPorTipo(infoAtestado.ObtenerIDdeRubro(Rubro), (int)Session["UsuarioID"]);
            ViewBag.Autores = infoAtestado.CargarAutoresAtestado(atestado.AtestadoID);
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
            ViewBag.Autores = infoAtestado.CargarAutoresAtestado(id);
            return View(atestado);
        }

        // POST: Libro/Borrar
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult Borrar(int id)
        {
            infoAtestado.BorrarAtestado(id);
            return RedirectToAction("Crear");
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

            if (Session["Autores"] == null)
            {
                Session["Autores"] = new List<AutorDTO>();
            }

            List<AutorDTO> autores = (List<AutorDTO>)Session["Autores"];
            autores.Add(autor);
            Session["Autores"] = autores;

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

            if (Session["Archivos"] == null)
            {
                Session["Archivos"] = new List<ArchivoDTO>();
            }

            ArchivoDTO ar = new ArchivoDTO
            {
                Nombre = Path.GetFileName(archivo.FileName),
                TipoArchivo = archivo.ContentType,
                Datos = bytes
            };
            List<ArchivoDTO> archivos = (List<ArchivoDTO>)Session["Archivos"];
            archivos.Add(ar);
            Session["Archivos"] = archivos;

            var jsonTest = JsonConvert.SerializeObject(ar);

            return Json(new
            {
                archivoJson = jsonTest
            });
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
