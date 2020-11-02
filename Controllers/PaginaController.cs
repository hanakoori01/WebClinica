using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClinica.Filter;
using WebClinica.Models;
using WebClinica.Models.ViewModel;

namespace Clinica.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class PaginaController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        public static List<Pagina> lista;
        private List<MenuController> ListaMenu = new List<MenuController>();
        public PaginaController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index(string mensaje)
        {
            CargarMenus();
            DeterminarUltimoRegistro();
            List<Pagina> listaPagina = new List<Pagina>();
            if (mensaje == null || mensaje == "")
            {
                listaPagina = (from pagina in _db.Pagina
                               where pagina.BotonHabilitado == 1
                               select new Pagina
                               {
                                   PaginaId = pagina.PaginaId,
                                   Menu = pagina.Menu,
                                   Accion = pagina.Accion,
                                   Controlador = pagina.Controlador
                               }).ToList();
                ViewBag.Menu = "";
            }
            else
            {
                listaPagina = (from pagina in _db.Pagina
                               where pagina.BotonHabilitado == 1
                               && pagina.Menu.Contains(mensaje)
                               select new Pagina
                               {
                                   PaginaId = pagina.PaginaId,
                                   Menu = pagina.Menu,
                                   Accion = pagina.Accion,
                                   Controlador = pagina.Controlador
                               }).ToList();
            }
            ViewBag.Menu = mensaje;
            lista = listaPagina;
            return View(lista);
        }
        private void DeterminarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<Pagina>().OrderByDescending(
                e => e.PaginaId).FirstOrDefault();
            if (ultimoRegistro != null)
            {
                ViewBag.ID = ultimoRegistro.PaginaId + 1;
            }
            else
            {
                ViewBag.ID = 1;
            }
        }


        public List<Pagina> ListarPaginas(string mensaje)
        {
            List<Pagina> listaPagina = new List<Pagina>();
            if (mensaje == null || mensaje == "")
            {
                listaPagina = (from pagina in _db.Pagina
                               where pagina.BotonHabilitado == 1
                               select new Pagina
                               {
                                   PaginaId = pagina.PaginaId,
                                   Menu = pagina.Menu,
                                   Accion = pagina.Accion,
                                   Controlador = pagina.Controlador
                               }).ToList();
            }
            else
            {
                listaPagina = (from pagina in _db.Pagina
                               where pagina.BotonHabilitado == 1
                               && pagina.Menu.Contains(mensaje)
                               select new Pagina
                               {
                                   PaginaId = pagina.PaginaId,
                                   Menu = pagina.Menu,
                                   Accion = pagina.Accion,
                                   Controlador = pagina.Controlador
                               }).ToList();
            }
            lista = listaPagina;
            return listaPagina;
        }

        public IActionResult Agregar()
        {
            CargarMenus();
            return View();
        }

        public IActionResult Eliminar(int paginaId)
        {

            Pagina oPagina = _db.Pagina.Where(p => p.PaginaId == paginaId).First();
            _db.Pagina.Remove(oPagina);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public int EliminarPagina(int paginaId)
        {
            int rpta = 0;
            string Error = "";
            try
            {
                Pagina oPagina = _db.Pagina.Where(p => p.PaginaId == paginaId).First();
                _db.Pagina.Remove(oPagina);
                _db.SaveChanges();
                rpta = 1;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                rpta = 0;
            }
            return rpta;
        }
        private void CargarMenus()
        {
            var list = new SelectList(new[]
                                     {
                                      new {ID="1",Name="Mantenimiento"},
                                      new{ID="2",Name="Consultas"},
                                      new{ID="3",Name="Citas"},
                                      new{ID="4",Name="Accesibilidad"},
                                             },
                               "ID", "Name", 1);
            ViewBag.ListaMenu = list;
        }
        public IActionResult Editar(int id)
        {
            CargarMenus();
            Pagina oPagina = new Pagina();
            oPagina = (from pagina in _db.Pagina
                       where pagina.PaginaId == id
                       select new Pagina
                       {
                           PaginaId = pagina.PaginaId,
                           Menu = pagina.Menu,
                           Accion = pagina.Accion,
                           Controlador = pagina.Controlador
                       }).First();
            return View(oPagina);
        }


        //realizar la inserciòn
        [HttpPost]
        public IActionResult Guardar(Pagina oPagina)
        {
            string nombreVista = "";
            int nveces = 0;
            try
            {
                if (oPagina.PaginaId == 0) nombreVista = "Agregar";
                else nombreVista = "Editar";
                if (oPagina.PaginaId == 0)
                {
                    nveces = _db.Pagina
                   .Where(p => p.Menu.ToUpper().Trim() ==
                   oPagina.Menu.ToUpper().Trim()).Count();
                }
                else
                {
                    nveces = _db.Pagina
                       .Where(p => p.Menu.ToUpper().Trim() ==
                       oPagina.Menu.ToUpper().Trim() &&
                       p.PaginaId != oPagina.PaginaId).Count();
                }

                if (!ModelState.IsValid || nveces >= 1)
                {
                    if (nveces >= 1) ViewBag.mensajeError =
                            "Ya existe el mensaje de la  pagina ingresada";
                    return View(nombreVista, oPagina);
                }
                else
                {
                    if (oPagina.PaginaId == 0)
                    {
                        Pagina _oPagina = new Pagina
                        {
                            Menu = oPagina.Menu,
                            Controlador = oPagina.Controlador,
                            Accion = oPagina.Accion,
                            BotonHabilitado = 1
                        };
                        _db.Pagina.Add(oPagina);
                        _db.SaveChanges();
                    }
                    else
                    {
                        Pagina _Opagina = _db.Pagina
                            .Where(p => p.PaginaId == oPagina.PaginaId).First();
                        _Opagina.Menu = oPagina.Menu;
                        _Opagina.Controlador = oPagina.Controlador;
                        _Opagina.Accion = oPagina.Accion;
                        _db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                return View(nombreVista, oPagina);
            }
            return RedirectToAction("Index");
        }

    }
}
