using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebClinica.Models;

namespace Clinica.Controllers
{
    public class ConsultaEspecialidadesController : Controller
    {
        private readonly DBClinicaAcmeContext _db;

        static List<Especialidad> lista = new List<Especialidad>();

        public ConsultaEspecialidadesController(DBClinicaAcmeContext db)
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
        //metodo que descarga el archivo excel
        public FileResult exportarExcel()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Especialidad", "Nombre", "Descripcion" };
            string[] nombrePropiedades = { "EspecialidadId", "Nombre", "Descripcion" };
            byte[] buffer = util.generarExcel(cabeceras, nombrePropiedades, lista);
            //content type mime xlsx google
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public FileResult exportarPDF()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Especialidad", "Nombre", "Descripcion" };
            string[] nombrePropiedades = { "EspecialidadId", "Nombre", "Descripcion" };
            string titulo = "Reporte de Especialidades";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }
    }
}
