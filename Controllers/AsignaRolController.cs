using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Clinica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Filter;
using WebClinica.Models;
using WebClinica.Models.ViewModel;

namespace WebClinica.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class AsignaRolController : Controller
    {
        private List<TipoUsuario> lista = new List<TipoUsuario>();
        private List<Pagina> listaPagina = new List<Pagina>();
        public static int UserType;
        private List<TipoUsuarioPaginas> listaTipoUsuarioPaginas = new List<TipoUsuarioPaginas>();
        private readonly DBClinicaAcmeContext _db;
        public AsignaRolController(DBClinicaAcmeContext db)
        {
            _db = db;
        }

        public List<Pagina> CargarPaginas()
        {
            //asegurarse que la consulta solo devuelva los perfiles
            //que no estan determinados para este tipo de usuario
            //o sea si ya tiene acceso a especialidades y médico
            //no tendrian que salir estas paginas.
            listaPagina = (from pagina in _db.Pagina
                           where pagina.BotonHabilitado == 1
                           select new Pagina
                           {
                               PaginaId = pagina.PaginaId,
                               Menu = pagina.Menu,
                               Accion = pagina.Accion,
                               Controlador = pagina.Controlador,
                               BotonHabilitado = pagina.BotonHabilitado
                           }).ToList();
            return listaPagina;
        }

        public List<TipoUsuarioPagina> RecuperarPaginas(int tipoUsuarioId)
        {
            List<TipoUsuarioPagina> Lista = new List<TipoUsuarioPagina>();
            Lista = (from tipoUsuarioPagina in _db.TipoUsuarioPagina
                     where tipoUsuarioPagina.TipoUsuarioId == tipoUsuarioId
                     orderby tipoUsuarioPagina.PaginaId
                     select new TipoUsuarioPagina
                     {
                         PaginaId = tipoUsuarioPagina.PaginaId,
                         TipoUsuarioPaginaId = tipoUsuarioPagina.TipoUsuarioPaginaId

                     }).ToList();
            return Lista;
        }

        public List<TipoUsuarioPaginas> ListarTipoUsuarioPaginas()
        {
            List<TipoUsuarioPaginas> Lista = new List<TipoUsuarioPaginas>();
            Lista = (from tipoUsuarioPagina in _db.TipoUsuarioPagina
                     join tipoUsuario in _db.TipoUsuario
                     on tipoUsuarioPagina.TipoUsuarioId equals tipoUsuario.TipoUsuarioId
                     join pagina in _db.Pagina
                     on tipoUsuarioPagina.PaginaId equals pagina.PaginaId
                     orderby pagina.PaginaId
                     select new TipoUsuarioPaginas
                     {
                         TipoUsuarioPaginaId = tipoUsuarioPagina.TipoUsuarioPaginaId,
                         PaginaId = tipoUsuarioPagina.PaginaId,
                         NombrePagina = pagina.Controlador,
                         TipoUsuarioId = tipoUsuarioPagina.TipoUsuarioId,
                         NombreTipoUsuario = tipoUsuario.Nombre,
                         BotonHabilitado = tipoUsuarioPagina.BotonHabilitado
                     }
                         ).ToList();
            return Lista;
        }

        private void CargarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<TipoUsuarioPagina>().OrderByDescending(e => e.TipoUsuarioPaginaId).FirstOrDefault();
            if (ultimoRegistro == null)
            {
                ViewBag.ID = 1;
            }
            else
            {
                ViewBag.ID = ultimoRegistro.TipoUsuarioPaginaId + 1;
            }
        }

        public string Registrar(int[] _PaginasAgregadas, int TipoUsuarioId)
        {
            string rpta = "OK";
            int encontrado;
            foreach (var item in _PaginasAgregadas)
            {
                encontrado = 1;
                for (var i = 0; i < RecuperarPaginas(TipoUsuarioId).Count && encontrado == 1; i++)
                {
                    if (item == RecuperarPaginas(TipoUsuarioId)[i].PaginaId)
                    {
                        encontrado = 1;
                    }
                    else
                    {
                        encontrado = 0;
                    }
                }
                if (encontrado == 1)
                {

                }
                else
                {
                    CargarUltimoRegistro();
                    TipoUsuarioPagina _TipoUsuarioPagina = new TipoUsuarioPagina()
                    {
                        TipoUsuarioPaginaId = ViewBag.ID,
                        TipoUsuarioId = TipoUsuarioId,
                        PaginaId = item,
                        BotonHabilitado = 1
                    };
                    _db.TipoUsuarioPagina.Add(_TipoUsuarioPagina);
                    _db.SaveChanges();
                }
            }
            return rpta;
        }

        public string Delete(int[] _PaginasEliminadas, int TipoUsuarioId)
        {
            string rpta = "OK";
            var Error = "";
            try
            {
                foreach (var item in _PaginasEliminadas)
                {
                    foreach (var items in RecuperarPaginas(TipoUsuarioId))
                    {
                        if (item == items.PaginaId)
                        {
                            TipoUsuarioPagina tipoUsuarioPagina = _db.TipoUsuarioPagina
                                .Where(e => e.TipoUsuarioPaginaId == items.TipoUsuarioPaginaId).First();
                            _db.TipoUsuarioPagina.Remove(tipoUsuarioPagina);
                            _db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return rpta;
        }

        public IActionResult Index(TipoUsuario oTipoUsuario)
        {
            List<TipoUsuario> listaTipoUsuario = new List<TipoUsuario>();
            listaTipoUsuario = (from tipousu in _db.TipoUsuario
                                where tipousu.BotonHabilitado == 1
                                select new TipoUsuario
                                {
                                    TipoUsuarioId = tipousu.TipoUsuarioId,
                                    Nombre = tipousu.Nombre,
                                    Descripcion = tipousu.Descripcion,
                                }).ToList();
            if (oTipoUsuario.Nombre != null && oTipoUsuario.TipoUsuarioId != 0)
            {

                ViewBag.Nombre = oTipoUsuario.Nombre;
                ViewBag.Descripcion = oTipoUsuario.Descripcion;
                ViewBag.TipoUsuarioId = oTipoUsuario.TipoUsuarioId;
            }

            lista = listaTipoUsuario;
            return View(listaTipoUsuario);
        }

        public IActionResult Listar(int id)
        {
            CargarPaginas();

            TipoUsuario _TipoUsuario = _db.TipoUsuario
            .Where(p => p.TipoUsuarioId == id).FirstOrDefault();

            ViewBag.TipoUsu = (int)_TipoUsuario.TipoUsuarioId;
            ViewBag.Usuario = _TipoUsuario.Nombre;
            ViewBag.Descripcion = _TipoUsuario.Descripcion;
            return View(listaPagina);
        }
    }
}
