using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    public class AsignaRolController : Controller
    {
        public static List<TipoUsuario> lista;
        private List<Pagina> LstPagina = new List<Pagina>();
        public static int UserType;
        private readonly DBClinicaAcmeContext _db;
        public AsignaRolController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index(TipoUsuario oTipoUsuario)
        {
            List<TipoUsuario> listaTipoUsu = new List<TipoUsuario>();
            listaTipoUsu = (from tipousu in _db.TipoUsuario
                            where tipousu.BotonHabilitado == 1
                            select new TipoUsuario
                            {
                                TipoUsuarioId = tipousu.TipoUsuarioId,
                                Nombre = tipousu.Nombre,
                            }).ToList();
            if (oTipoUsuario.Nombre != null && oTipoUsuario.TipoUsuarioId != 0)
            {

                ViewBag.Nombre = oTipoUsuario.Nombre;
                ViewBag.TipoUsuarioId = oTipoUsuario.TipoUsuarioId;
            }

            lista = listaTipoUsu;
            return View(listaTipoUsu);
        }
        private List<Pagina> CargarPaginas()
        {
            //asegurarse que la consulta solo devuelva los perfiles
            //que no estan determinados para este tipo de usuario
            //o sea si ya tiene acceso a especialidades y médico
            //no tendrian que salir estas paginas.
            LstPagina = (from pagina in _db.Pagina
                         where pagina.BotonHabilitado == 1
                         select new Pagina
                         {
                             PaginaId = pagina.PaginaId,
                             Menu = pagina.Menu,
                             Accion = pagina.Accion,
                             Controlador = pagina.Controlador
                         }).ToList();
            return LstPagina;
        }

        public IActionResult Listar(int? id)
        {
            CargarPaginas();
            TipoUsuario _TipoUsuario = _db.TipoUsuario
            .Where(p => p.TipoUsuarioId == id).FirstOrDefault();

            ViewBag.TipoUsu = (int)_TipoUsuario.TipoUsuarioId;
            ViewBag.Usuario = _TipoUsuario.Nombre;
            return View(LstPagina);
        }
        public string Registrar(int[] _Paginas)
        {
            string rpta = "OK";
            foreach (var item in _Paginas)
            {

            }
            return rpta;
        }
    }
}
