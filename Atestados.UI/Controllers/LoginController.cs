using Atestados.Objetos.Clases;
using Atestados.UIProcess.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace Atestados.UI.Controllers
{
    public class LoginController : Controller
    {
        #region Instancias
        //Instancia a la clase de Servicios en UIProcess
        private AccesoSeguridad ServiciosSeguridad = new AccesoSeguridad();
        //Objeto de retorno
        oRespuesta<string> respuestaValidacion = new oRespuesta<string>();

        //Debido que el usuario aun no está logueado, se usa un dummy inicial al logueo.
        //Luego de loguearse, cambia los datos al usuario que se encuentra logueado.
        int idUsuarioLogueado = 1;
        string usuarioLogueado = "sistemaLogueo@itcr.ac.cr";
        public string sessionId = ConfigurationManager.AppSettings["SessionID"];

        public void InitializeController(RequestContext context)
        {
            base.Initialize(context);
        }
        #endregion

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaginaInvalida()
        {
            return View();
        }


        /// <summary>
        /// Login de usuario - valida las credenciales ingresadas
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="contrasena"></param>
        /// <returns></returns>
        public JsonResult ValidarUsuario(string usuario, string contrasena)
        {
            try
            {
                string extITCR = WebConfigurationManager.AppSettings["extITCR"].ToString();
                string extTEC = WebConfigurationManager.AppSettings["extTEC"].ToString();
                string extESTUDTEC = WebConfigurationManager.AppSettings["exESTUDTEC"].ToString();

                if (!usuario.Contains("@" + extITCR))
                {
                    usuario = usuario + "@" + extITCR; //Por ahora solo funcionarios
                }


                //Validación de nulos
                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
                {
                    respuestaValidacion.CodigoRespuesta = -1;
                    respuestaValidacion.MensajeRespuesta = Utilitarios.Mensajes.Mensajes.ValidacionNulos;
                    return new JsonResult { Data = respuestaValidacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

                if (!string.IsNullOrEmpty(usuario))
                {
                    if (usuario.Contains(extTEC))
                    {
                        usuario = usuario.Replace(extTEC, extITCR);
                    }
                }

                //Limpieza de datos de logueo
                usuario = usuario.Trim();
                //contrasena = contrasena.Trim();
                usuario = usuario.ToLower();

                //Prevención inicial de inyección de código
                bool validaCaracteresUsuario = Utilitarios.Clases.Utilitarios.ValidarCaracteresEspeciales(usuario);
                bool validaCaracteresPassword = Utilitarios.Clases.Utilitarios.ValidarCaracteresEspeciales(contrasena);

                //Validación caracteres usuario
                if (validaCaracteresUsuario)
                {
                    respuestaValidacion.CodigoRespuesta = -1;
                    respuestaValidacion.MensajeRespuesta = Utilitarios.Mensajes.Mensajes.ValidacionCaracteresUsuario;
                    return new JsonResult { Data = respuestaValidacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                //Validación caracteres contraseña
                else if (validaCaracteresPassword)
                {
                    respuestaValidacion.CodigoRespuesta = -1;
                    respuestaValidacion.MensajeRespuesta = Utilitarios.Mensajes.Mensajes.ValidacionCaracteresContrasena;
                    return new JsonResult { Data = respuestaValidacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

                //Acceso a servicio para validación de usuario
                Session[ConfigurationManager.AppSettings["SessionID"]] = HttpContext.Session.SessionID;
                var consultaUsuario = ServiciosSeguridad.AutenticarUsuario(usuario, contrasena, idUsuarioLogueado, usuarioLogueado, Utilitarios.Clases.Utilitarios.GetIpAddress(), Session[sessionId].ToString());
                //Fue validado
                if (consultaUsuario.CodigoRespuesta == 0)
                {
                    //Si el usuario es válido crea la sesión
                    Session[ConfigurationManager.AppSettings["CodigoUsuarioLogueado"]] = consultaUsuario.ObjetoRespuesta.IdUsuario;
                    Session["sesionCodigoUsuarioLogueado"] = consultaUsuario.ObjetoRespuesta.IdUsuario;
                    Session[ConfigurationManager.AppSettings["usuarioLogueado"]] = usuario;
                    Session["NombreCompletoUsuario"] = consultaUsuario.ObjetoRespuesta.Nombre + " " + consultaUsuario.ObjetoRespuesta.Apellido1 + " " + consultaUsuario.ObjetoRespuesta.Apellido2;
                    //generarPermisosMenu(usuario);
                    respuestaValidacion.CodigoRespuesta = 0;
                    respuestaValidacion.MensajeRespuesta = string.Empty;

                    //Consultar Id Persona
                    var respIdPersona = ServiciosSeguridad.ConsultarIdPersona(consultaUsuario.ObjetoRespuesta.IdUsuario);
                    Session["sesionIdPersona"] = respIdPersona.ObjetoRespuesta.Persona.IdPersona;


                    return new JsonResult { Data = respuestaValidacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                   
                }
                else
                {
                    //Si el usuario no es válido no crea el dato de sesión
                    Session[ConfigurationManager.AppSettings["CodigoUsuarioLogueado"]] = "0";
                    Session[ConfigurationManager.AppSettings["usuarioLogueado"]] = null;
                    respuestaValidacion.CodigoRespuesta = -1;
                    respuestaValidacion.MensajeRespuesta = consultaUsuario.DescripcionRespuesta;
                    return new JsonResult { Data = respuestaValidacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            catch (Exception ex)
            {
                respuestaValidacion.CodigoRespuesta = -1;
                respuestaValidacion.MensajeRespuesta = Utilitarios.Mensajes.Mensajes.MensajeGenerico + " - " + ex.Message;
                return new JsonResult { Data = respuestaValidacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }


        /// <summary>
        /// Obtiene los permisos /menu del usuario
        /// </summary>
        /// <param name="idPadre"></param>
        /// <returns></returns>
        public JsonResult ConsultarPermisosUsuario(int idPadre)
        {
            try
            {
                //Acceso a servicios
                var respuesta = ServiciosSeguridad.ConsultarPermisosUsuario(0, int.Parse(Session[ConfigurationManager.AppSettings["CodigoUsuarioLogueado"]].ToString()), idPadre, 0, int.Parse(Session[ConfigurationManager.AppSettings["CodigoUsuarioLogueado"]].ToString()), Session[ConfigurationManager.AppSettings["usuarioLogueado"]].ToString(), Utilitarios.Clases.Utilitarios.GetIpAddress(), "");

                if (respuesta.ObjetoRespuesta.Count() <= 0)
                {
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                    Response.Cache.SetNoStore();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

                    respuestaValidacion.CodigoRespuesta = -1;
                    respuestaValidacion.MensajeRespuesta = Utilitarios.Mensajes.Mensajes.ValidacionPermisosUsuario;
                    return new JsonResult { Data = respuestaValidacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

                return new JsonResult { Data = respuesta, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                respuestaValidacion.CodigoRespuesta = -1;
                respuestaValidacion.MensajeRespuesta = Utilitarios.Mensajes.Mensajes.MensajeGenerico + " - " + ex.Message;
                return new JsonResult { Data = respuestaValidacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }


        //Consultar roles
        public JsonResult ConsultarEsAdminAtestados()
        {
            try
            {
                //Acceso a servicios
                int codUsuarioLogueado = int.Parse(Session[ConfigurationManager.AppSettings["CodigoUsuarioLogueado"]].ToString());
                string usuarioLogueado = Session[ConfigurationManager.AppSettings["usuarioLogueado"]].ToString();

                var respuesta = ServiciosSeguridad.ConsultarRolesUsuario(codUsuarioLogueado, codUsuarioLogueado,usuarioLogueado, Utilitarios.Clases.Utilitarios.GetIpAddress(), "");

                if (respuesta.ObjetoRespuesta.Count() <= 0)
                {
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                    Response.Cache.SetNoStore();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

                    respuestaValidacion.CodigoRespuesta = -1;
                    respuestaValidacion.MensajeRespuesta = Utilitarios.Mensajes.Mensajes.ValidacionPermisosUsuario;
                    respuestaValidacion.ObjetoRespuesta = null;
                    return new JsonResult { Data = respuestaValidacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    Session["EsAdministrador"] = 0;
                    int codRolAdminAtestados = int.Parse(ConfigurationManager.AppSettings["CodRolAdim"].ToString());
                    foreach (var itm in respuesta.ObjetoRespuesta)
                    {                     
                        if (itm.IdRol == codRolAdminAtestados)
                        {
                            Session["EsAdministrador"] = 1;
                        }
                    }
                }

                //porque todo queda en session
                int esAdmin = int.Parse(Session["EsAdministrador"].ToString());
                if (esAdmin == 1)
                {
                    respuesta.CodigoRespuesta = 0;
                    respuesta.DescripcionRespuesta = string.Empty;
                    respuesta.ObjetoRespuesta = null;
                }
                else
                {
                    respuesta.CodigoRespuesta = -1;
                    respuesta.DescripcionRespuesta = string.Empty;
                    respuesta.ObjetoRespuesta = null;
                }


                return new JsonResult { Data = respuesta, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                respuestaValidacion.CodigoRespuesta = -1;
                respuestaValidacion.MensajeRespuesta = Utilitarios.Mensajes.Mensajes.MensajeGenerico + " - " + ex.Message;
                return new JsonResult { Data = respuestaValidacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }


        //Cerrar sesión de aplicación
        public ActionResult CerrarSesion()
        {
            //inserta en bitácora del sistema de seguridad la salida
            if (Session[ConfigurationManager.AppSettings["usuarioLogueado"]] != null)
            {
                // ServiciosSeguridad.InsertarBitacoraSistema("Cierre de sesión Usuario : " + Session[ConfigurationManager.AppSettings["UsuarioLogueado"]].ToString(), Constantes.CodigosBitacora.INACTIVAR, "LoginController - Interno", "CerrarSesion", false, Convert.ToInt32(Session[ConfigurationManager.AppSettings["CodigoUsuarioLogueado"]]), Session[ConfigurationManager.AppSettings["UsuarioLogueado"]].ToString(), Utilitarios.Clases.Utilitarios.GetIpAddress(), Session[sessionId].ToString(), null);
            }



            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

            return RedirectToAction("Index", "Login");
        }
    }
}