using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    public class AsignaRoleController : Controller
    {
        public static List<TipoUsuarioPagina> Lista;
        private readonly DBClinicaAcmeContext _db;
        public AsignaRoleController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            string nombrecontroller = ControllerContext.ActionDescriptor.ControllerName;
            List<Pagina> lista = Utilitarios.ListarBotonesDatos(nombrecontroller);
            ViewBag.botones = lista.Select(p => p.BotonId).ToList();
            LlenarTipoUsuario();
            return View();
        }

        public string GuardarDatos(int id, int[] idBotones)
        {
            //Error (rpta="OK")
            string rpta = "";
            try
            {
                using (var transaction = new TransactionScope())
                {
                    if (ModelState.IsValid)
                    {
                        List<TipoUsuarioPaginaBoton>
                        lista = _db.TipoUsuarioPaginaBoton.Where(p =>
                          p.TipoUsuarioPaginaId == id).ToList();
                        if (lista != null)
                        {
                            foreach (TipoUsuarioPaginaBoton obj in lista)
                            {
                                obj.BotonHabilitado = 0;
                            }
                        }
                        foreach (int num in idBotones)
                        {
                            int ncantidad = _db.TipoUsuarioPaginaBoton.Where(
                                p => p.TipoUsuarioPaginaId
                                          == id
                                           && p.BotonId == num).Count();
                            if (ncantidad == 0)
                            {
                                //idpagina 1
                                TipoUsuarioPaginaBoton oTipoUsuarioPagina = new TipoUsuarioPaginaBoton();
                                oTipoUsuarioPagina.TipoUsuarioPaginaId = id;
                                oTipoUsuarioPagina.BotonId = num;
                                oTipoUsuarioPagina.BotonHabilitado = 1;
                                _db.TipoUsuarioPaginaBoton.Add(oTipoUsuarioPagina);
                            }
                            else
                            {
                                TipoUsuarioPaginaBoton o = _db.TipoUsuarioPaginaBoton.Where(
                                 p => p.TipoUsuarioPaginaId == id
                                   && p.BotonId == num).First();
                                o.BotonHabilitado = 1;
                            }
                        }
                        _db.SaveChanges();
                        transaction.Complete();
                        rpta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }


        public List<Boton> ListarBotones()
        {
            List<Boton> ListaBoton = new List<Boton>();
            ListaBoton = (from boton in _db.Boton
                          where boton.BotonHabilitado == 1
                          select new Boton
                          {
                              BotonId = boton.BotonId,
                              Nombre = boton.Nombre,
                          }).ToList();
            return ListaBoton;
        }

        public List<Boton> RecuperarBotones(int paginatipousuarioid)
        {
            List<Boton> Lista = new List<Boton>();
            Lista = (from tipo in _db.TipoUsuarioPaginaBoton
                     where tipo.BotonHabilitado == 1
                     && tipo.TipoUsuarioPaginaId == paginatipousuarioid
                     select new Boton
                     {
                         BotonId = (int)tipo.BotonId
                     }).ToList();
            return Lista;
        }

        public void LlenarTipoUsuario()
        {
            List<SelectListItem> ListaTipoUsuario = new List<SelectListItem>();
            ListaTipoUsuario = (from tipousu in _db.TipoUsuario
                                where tipousu.BotonHabilitado == 1
                                select new SelectListItem
                                {
                                    Text = tipousu.Nombre,
                                    Value = tipousu.TipoUsuarioId.ToString()
                                }).ToList();
            ListaTipoUsuario.Insert(0, new SelectListItem
            {
                Text = "--Seleccione--",
                Value = ""
            });
            ViewBag.ListaTipoUsu = ListaTipoUsuario;
        }

        public List<TipoUsuarioPagina> ListaPagTipoUsu(int? tipousuid)
        {

            List<TipoUsuarioPagina> ListaTipoUsuario =
                          new List<TipoUsuarioPagina>();
            if (tipousuid == null)
            {
                ListaTipoUsuario = (from paginatipousu in _db.TipoUsuarioPagina
                                    join pagina in _db.Pagina
                                    on paginatipousu.PaginaId equals pagina.PaginaId
                                    join tipousu in _db.TipoUsuario
                                    on paginatipousu.TipoUsuarioId equals tipousu.TipoUsuarioId
                                    where paginatipousu.BotonHabilitado == 1
                                    select new TipoUsuarioPagina
                                    {
                                        TipoUsuarioPaginaId = paginatipousu.TipoUsuarioPaginaId,

                                        NombrePagina = pagina.Menu,
                                        NombreTipoUsuario = tipousu.Nombre
                                    }).ToList();
            }
            else
            {
                ListaTipoUsuario = (from paginatipousu in _db.TipoUsuarioPagina
                                    join pagina in _db.Pagina
                                    on paginatipousu.PaginaId equals pagina.PaginaId
                                    join tipousu in _db.TipoUsuario
                                    on paginatipousu.TipoUsuarioId equals tipousu.TipoUsuarioId
                                    where paginatipousu.BotonHabilitado == 1
                                    && paginatipousu.TipoUsuarioId == tipousuid
                                    select new TipoUsuarioPagina
                                    {
                                        TipoUsuarioPaginaId = paginatipousu.TipoUsuarioPaginaId,
                                        NombrePagina = pagina.Menu,
                                        NombreTipoUsuario = tipousu.Nombre
                                    }).ToList();
            }
            return ListaTipoUsuario;
        }
    }
}
