using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Models;

namespace Clinica.Controllers
{
    public class PaginaController : Controller
    {
        public static List<Pagina> lista;
        private readonly DBClinicaAcmeContext _db;

        public PaginaController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index(string mensaje)
        {
            List<Pagina> listaPagina = new List<Pagina>();
            if (mensaje == null || mensaje == "")
            {
                listaPagina = (from pagina in _db.Pagina
                               where pagina.BotonHabilitado == 1
                               select new Pagina
                               {
                                   PaginaId = pagina.PaginaId,
                                   Mensaje = pagina.Mensaje,
                                   Accion = pagina.Accion,
                                   Controlador = pagina.Controlador
                               }).ToList();
                ViewBag.Mensaje = "";
            }
            else
            {
                listaPagina = (from pagina in _db.Pagina
                               where pagina.BotonHabilitado == 1
                               && pagina.Mensaje.Contains(mensaje)
                               select new Pagina
                               {
                                   PaginaId = pagina.PaginaId,
                                   Mensaje = pagina.Mensaje,
                                   Accion = pagina.Accion,
                                   Controlador = pagina.Controlador
                               }).ToList();
            }
            ViewBag.Mensaje = mensaje;
            lista = listaPagina;
            return View(lista);
        }






    ////mostrar el formulario
    //public IActionResult Agregar()
    //{
    //    return View();
    //}

    //public IActionResult Eliminar(int iidpagina)
    //{
    //    using (BDHospitalContext db = new BDHospitalContext())
    //    {

    //        Pagina oPagina = db.Pagina.Where(p => p.Iidpagina == iidpagina).First();
    //        db.Pagina.Remove(oPagina);
    //        db.SaveChanges();
    //    }

    //    return RedirectToAction("Index");
    //}

    //public int EliminarPagina(int iidpagina)
    //{
    //    int rpta = 0;
    //    try
    //    {
    //        using (BDHospitalContext db = new BDHospitalContext())
    //        {

    //            Pagina oPagina = db.Pagina.Where(p => p.Iidpagina == iidpagina).First();
    //            db.Pagina.Remove(oPagina);
    //            db.SaveChanges();
    //            rpta = 1;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        rpta = 0;
    //    }


    //    return rpta;
    //}

    //public IActionResult Editar(int id)
    //{
    //    Pagina oPagina = new Pagina();
    //    using (BDHospitalContext db = new BDHospitalContext())
    //    {
    //        oPagina = (from pagina in db.Pagina
    //                      where pagina.Iidpagina == id
    //                      select new Pagina
    //                      {
    //                          iidPagina = pagina.Iidpagina,
    //                          mensaje = pagina.Mensaje,
    //                          accion = pagina.Accion,
    //                          controlador = pagina.Controlador
    //                      }).First();
    //    }
    //    return View(oPagina);
    //}


    ////realizar la inserciòn
    //[HttpPost]
    //public IActionResult Guardar(Pagina oPagina)
    //{
    //    string nombreVista = "";
    //    int nveces = 0;
    //    try
    //    {
    //        if (oPagina.iidPagina == 0) nombreVista = "Agregar";
    //        else nombreVista = "Editar";
    //        using (BDHospitalContext db = new BDHospitalContext())
    //        {
    //            if (oPagina.iidPagina == 0)
    //            {
    //                nveces = db.Pagina
    //                    .Where(p => p.Mensaje.ToUpper().Trim() ==
    //                    oPagina.mensaje.ToUpper().Trim()).Count();
    //            }
    //            else
    //            {
    //                nveces = db.Pagina
    //                   .Where(p => p.Mensaje.ToUpper().Trim() ==
    //                   oPagina.mensaje.ToUpper().Trim() &&
    //                   p.Iidpagina != oPagina.iidPagina).Count();
    //            }

    //            if (!ModelState.IsValid || nveces >= 1)
    //            {
    //                if (nveces >= 1) oPagina.mensajeError =
    //                        "Ya existe el mensaje de la  pagina ingresada";
    //                return View(nombreVista, oPagina);
    //            }
    //            else
    //            {
    //                if (oPagina.iidPagina == 0)
    //                {
    //                    Pagina oPagina = new Pagina();
    //                    oPagina.Mensaje = oPagina.mensaje;
    //                    oPagina.Controlador = oPagina.controlador;
    //                    oPagina.Accion = oPagina.accion;
    //                    oPagina.Bhabilitado = 1;
    //                    db.Pagina.Add(oPagina);
    //                    db.SaveChanges();
    //                }
    //                else
    //                {
    //                    Pagina opagina = db.Pagina
    //                        .Where(p => p.Iidpagina == oPagina.iidPagina).First();
    //                    opagina.Mensaje = oPagina.mensaje;
    //                    opagina.Controlador = oPagina.controlador;
    //                    opagina.Accion = oPagina.accion;
    //                    db.SaveChanges();
    //                }
    //            }


    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        return View(nombreVista, oPagina);
    //    }
    //    return RedirectToAction("Index");
    //}

}
}
