using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Models;

namespace Clinica.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        private readonly DBClinicaAcmeContext _db;

        public LoginController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public string _Login(string user, string pass)
        {
            string rpta = "";
            string claveCifrada = Utilitarios.CifrarDatos(pass);
            try
            {

                int nVeces = _db.Usuario.Where(u => u.Nombre == user
                && u.Password == claveCifrada).Count();
                if (nVeces != 0)
                {
                    rpta = "OK";
                    Usuario User = _db.Usuario.Where(u => u.Nombre == user
                            && u.Password == claveCifrada).First();
                    HttpContext.Session.SetString("usuarioId", User.UsuarioId.ToString());
                    HttpContext.Session.SetString("nombreUsuario", User.Nombre);
                    //int idTipo = User.TipoUsuarioId;
                    List<Pagina> lista = new List<Pagina>();
                    lista = (from pgt in _db.TipoUsuarioPagina
                             join pagina in _db.Pagina
                             on pgt.PaginaId equals pagina.PaginaId
                             where pgt.BotonHabilitado == 1
                             && pgt.TipoUsuarioId == User.TipoUsuarioId
                             select new Pagina
                             {
                                 Menu = pagina.Menu,
                                 Controlador = pagina.Controlador,
                                 Accion = pagina.Accion
                             }).ToList();
                    Utilitarios.listaPagina = lista;
                    List<Pagina> ListaBoton = (from pgtb in _db.TipoUsuarioPaginaBoton
                                               join tup in _db.TipoUsuarioPagina
                                               on pgtb.TipoUsuarioPaginaBotonId
                                               equals tup.TipoUsuarioPaginaId
                                               join pag in _db.Pagina
                                               on tup.PaginaId equals pag.PaginaId
                                               where tup.TipoUsuarioId == User.UsuarioId
                                               && pgtb.BotonHabilitado == 1
                                               && tup.BotonHabilitado == 1
                                               select new Pagina
                                               {
                                                   PaginaId = (int)tup.PaginaId,
                                                   BotonId = (int)pgtb.BotonId,
                                                   Controlador = pag.Controlador
                                               }).ToList();
                    Utilitarios.listaBotonesPagina = ListaBoton;

                    Utilitarios.MenuMant = "";
                    Utilitarios.MenuCons = "";
                    Utilitarios.MenuAcce = "";
                    Utilitarios.ListaMenu.Clear();
                    Utilitarios.ListaController.Clear();
                    Utilitarios.ListaAccion.Clear();
                    ViewBag.User = User.Nombre;
                    foreach (Pagina _Pagina in lista)
                    {
                        Utilitarios.ListaMenu.Add(_Pagina.Menu);
                        Utilitarios.ListaController.Add(_Pagina.Controlador);
                        Utilitarios.ListaAccion.Add(_Pagina.Accion);
                        if (_Pagina.Controlador == "Especialidad" ||
                            _Pagina.Controlador == "Medico" ||
                            _Pagina.Controlador == "Enfermedades" ||
                            _Pagina.Controlador == "Pacientes")

                        {
                            Utilitarios.MenuMant = "Mantenimiento";
                        }
                        if (_Pagina.Controlador == "ConsultaEspecialidades" ||
                            _Pagina.Controlador == "ConsultaCitas" ||
                            _Pagina.Controlador == "ConsultaPacientes")
                        {
                            Utilitarios.MenuCons = "Consultas";
                        }
                        if (_Pagina.Controlador == "TipoUsuarios" ||
                            _Pagina.Controlador == "Usuarios" ||
                            _Pagina.Controlador == "AsignaRol" ||
                            _Pagina.Controlador == "DeterminarRol" ||
                            _Pagina.Controlador == "Pagina")
                        {
                            Utilitarios.MenuAcce = "Accesibilidad";
                        }

                    }
                    //https://www.tiracodigo.com/index.php/programacion/mvc/formas-de-almacenar-datos-temporales-en-asp-net-mvc-viewdata-viewbag-tempdata-y-session
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }
        public ActionResult CerrarSesion()
        {
            HttpContext.Session.Remove("usuarioId");
            return RedirectToAction("Index");
        }
    }
}
