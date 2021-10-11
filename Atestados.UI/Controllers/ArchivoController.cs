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

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase archivo)
        {
            byte[] bytes;
            using(BinaryReader br = new BinaryReader(archivo.InputStream))
            {
                bytes = br.ReadBytes(archivo.ContentLength);
            }
            info.GuardarArchivo(new Archivo
            {
                AtestadoID = 1,
                Nombre = Path.GetFileName(archivo.FileName),
                TipoArchivo = archivo.ContentType,
                Datos = bytes
            });
            return RedirectToAction("Index");
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
