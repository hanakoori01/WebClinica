using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Filter;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class ConsultaEspecialidadController : Controller
    {
        static List<Especialidad> lista = new List<Especialidad>();

        private readonly DBClinicaAcmeContext _db;
      

        public ConsultaEspecialidadController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public List<Especialidad> BuscarEspecialidad(string nombreEspecialidad)
        {
            List<Especialidad> listaEspecialidad = new List<Especialidad>();
            if (nombreEspecialidad == null || nombreEspecialidad.Length == 0)
            {
                listaEspecialidad = (from especialidad in _db.Especialidad
                                     select new Especialidad
                                     {
                                         EspecialidadId = especialidad.EspecialidadId,
                                         Nombre = especialidad.Nombre,
                                         Descripcion = especialidad.Descripcion
                                     }).ToList();

                ViewBag.Especialidad = "";
            }
            else
            {
                listaEspecialidad = (from especialidad in _db.Especialidad
                                     where especialidad.Nombre.Contains(nombreEspecialidad)
                                     select new Especialidad
                                     {
                                         EspecialidadId = especialidad.EspecialidadId,
                                         Nombre = especialidad.Nombre,
                                         Descripcion = especialidad.Descripcion
                                     }).ToList();
                ViewBag.Especialidad = nombreEspecialidad;
            }
            lista = listaEspecialidad;
            return listaEspecialidad;
        }

        public IActionResult Index()
        {
            List<Especialidad> listaEspecialidad = new List<Especialidad>();
            listaEspecialidad = BuscarEspecialidad("");
            return View(listaEspecialidad);
        }

        public FileResult exportarExcel()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Nombre", "Descripcion"};
            string[] nombrePropiedades = { "Nombre", "Descripcion" };
            byte[] buffer = util.generarExcel(cabeceras, nombrePropiedades, lista);
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public FileResult exportarPDF()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Nombre", "Descripcion" };
            string[] nombrePropiedades = { "Nombre", "Descripcion" };
            string titulo = "Reporte de especialidad";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }
    }
}
