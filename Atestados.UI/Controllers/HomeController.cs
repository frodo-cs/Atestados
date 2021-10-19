using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Atestados.UIProcess.Clases;
using Atestados.Objetos.Clases;
using Atestados.Utilitarios.Clases;
using Atestados.Utilitarios.Constantes;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System.Web.WebPages;
using Atestados.Objetos.Dtos;

namespace Atestados.UI.Controllers
{
    public class HomeController : Controller
    {
        #region Instancias
        //Instancia a la clase de Servicios en UIProcess
        private AccesoSeguridad ServiciosSeguridad = new AccesoSeguridad();
        //Objeto de retorno
        oRespuesta<string> respuestaValidacion = new oRespuesta<string>();

        //Debido que el usuario aun no está logueado, se usa un dummy inicial al logueo.
        //Luego de loguearse, cambia los datos al usuario que se encuentra logueado.
        public int idUsuarioLogueado = 1;
        public string usuarioLogueado = "sistemaLogueo@itcr.ac.cr";
        public string sessionNombreUsuario = "NombreUsuario";
        public string sessionId = ConfigurationManager.AppSettings["SessionID"];
        #endregion

        public ActionResult Index()
        {
            /*if (Session["usuarioLogueado"] != null)
            {
                ViewBag.NombreUsuario = Session["usuarioLogueado"].ToString();
                ViewBag.NombreCompleto = Session["usuarioLogueado"].ToString();
                //inicializa el controller de Login para obtener el menú.
                LoginController controllerPermiso = new LoginController();
                controllerPermiso.InitializeController(this.Request.RequestContext);
                JsonResult respuesta = controllerPermiso.ConsultarPermisosUsuario(0);
                Session["PermisosMenu"] = respuesta.Data;
                Session["IdTipoPermiso"] = ConfigurationManager.AppSettings["TipoPermiso"];

                string _url = Request.Url.ToString();
                //Consulta permiso de acceso a URL
                //bool tieneAcceso = TieneAccesoAView(respuesta.Data, _url);
                bool tieneAcceso = true;
                if (tieneAcceso == false) { return RedirectToAction("PaginaInvalida", "Login"); }

                return View();
            }
            else
            {
                //return RedirectToAction("Index", "Login");
                
            } */
            return RedirectToAction("Index", "Funcionario");
        }

        public ActionResult Dashboard()
        {
            /*if (Session["usuarioLogueado"] != null)
            {
                ViewBag.NombreUsuario = Session["usuarioLogueado"].ToString();
                ViewBag.NombreCompleto = Session["usuarioLogueado"].ToString();
                //inicializa el controller de Login para obtener el menú.
                LoginController controllerPermiso = new LoginController();
                controllerPermiso.InitializeController(this.Request.RequestContext);
                JsonResult respuesta = controllerPermiso.ConsultarPermisosUsuario(0);
                Session["PermisosMenu"] = respuesta.Data;
                Session["IdTipoPermiso"] = ConfigurationManager.AppSettings["TipoPermiso"];

                string _url = Request.Url.ToString();
                //Consulta permiso de acceso a URL
                //bool tieneAcceso = TieneAccesoAView(respuesta.Data, _url);
                bool tieneAcceso = true;
                if (tieneAcceso == false) { return RedirectToAction("PaginaInvalida", "Login"); }

                return View();
            }
            else
            {
                //return RedirectToAction("Index", "Login");
                
            } */
            Session["Archivos"] = new List<ArchivoDTO>();
            Session["Autores"] = new List<AutorDTO>();
            return RedirectToAction("Index", "Funcionario");
        }


        public JsonResult ListaUsuarios(string correoElectronico)
        {
            try
            {
                //Session
                if (Session["usuarioLogueado"] == null)
                {
                    respuestaValidacion.CodigoRespuesta = -99;
                    respuestaValidacion.MensajeRespuesta = string.Empty;
                    return new JsonResult { Data = respuestaValidacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

                //Acceso a servicios
                var respuesta = ServiciosSeguridad.UsuariosDisponibles(0, 1, correoElectronico, true, false, "1.1.1.1", Session["SessionID"].ToString(), int.Parse(Session["CodigoUsuarioLogueado"].ToString()), Session["UsuarioLogueado"].ToString());

                if (respuesta.CodigoRespuesta == -1 && respuesta.ObjetoRespuesta == null && respuesta.DescripcionRespuesta.Contains("No posee"))
                {
                    respuestaValidacion.CodigoRespuesta = -1;
                    respuestaValidacion.MensajeRespuesta = respuesta.DescripcionRespuesta;
                    return new JsonResult { Data = respuestaValidacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

                var info = respuesta.ObjetoRespuesta.ListaUsuarios;

                foreach (var item in info)
                {
                    DateTime Fecha1 = Convert.ToDateTime(item.FechaCreacion);
                    string fechaI;
                    if (item.FechaCreacion != null)
                    {

                        Fecha1 = Convert.ToDateTime(item.FechaCreacion);
                        fechaI = Fecha1.Date.ToString("dd-MM-yyyy");
                        item.Fecha = fechaI;
                    }
                    else
                    {
                        fechaI = null;
                    }
                }

                return Json(new { CodigoRespuesta = respuesta.CodigoRespuesta, DescripcionRespuesta = respuesta.DescripcionRespuesta, listado = info }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                respuestaValidacion.CodigoRespuesta = -1;
                respuestaValidacion.MensajeRespuesta = "";
                return new JsonResult { Data = respuestaValidacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }         
        }


        public JsonResult Consultar(string idConfiguracion)
        {
            var respuesta = new oRespuesta<List<Configuracion>>();
            try
            {
                int idPersona = int.Parse(Session["sesionIdPersona"].ToString()); //En caso de no ser admin se usa
                string nombreCompletoSolicitante = Session["NombreCompletoUsuario"].ToString();

                //aplica validaciones si las hay

                if (idConfiguracion.IsEmpty())
                {
                    respuesta.CodigoRespuesta = Constantes.Respuesta.CODIGOERROR;
                    respuesta.Estado = false;
                    respuesta.MensajeRespuesta = Utilitarios.Mensajes.Mensajes.Atestados_Parametros_invalidos;
                    return Json(new { respuesta }, JsonRequestBehavior.AllowGet);
                }

                //definición de variables que serán enviadas al método
                int idConfig = Int32.Parse(idConfiguracion);




                //Acceso a servicios
                respuesta = ServiciosSeguridad.ObtenerConfiguracion(idConfig);

                return Json(new { respuesta }, JsonRequestBehavior.AllowGet);


            }
            catch
            {
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }


        #region Validación de acceso
        /// <summary>
        /// Realiza la validación si el usuario tiene acceso a la vista
        /// </summary>
        /// <param name="respuestaPermisos"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool TieneAccesoAView(object respuestaPermisos, string url)
        {
            UIProcess.WCF_Seguridad.TEC_MensajeOfListOfTEC_Permiso permisosObtenidos = new UIProcess.WCF_Seguridad.TEC_MensajeOfListOfTEC_Permiso();
            permisosObtenidos = (UIProcess.WCF_Seguridad.TEC_MensajeOfListOfTEC_Permiso)respuestaPermisos;

            if (permisosObtenidos.CodigoRespuesta == Constantes.Respuesta.CODIGOERROR)
            {
                return false;
            }

            bool tieneAcceso = false;
            foreach (var perm in permisosObtenidos.ObjetoRespuesta)
            {
                if (url.ToLower().Contains(perm.UrlPagina.ToLower())) { tieneAcceso = true; }
            }
            return tieneAcceso;
        }

    #endregion
    }
}