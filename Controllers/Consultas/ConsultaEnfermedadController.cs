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
    public class ConsultaEnfermedadController : Controller
    {
        private readonly DBClinicaAcmeContext _db;

        static List<Enfermedad> lista = new List<Enfermedad>();

        public ConsultaEnfermedadController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public List<Enfermedad> BuscarEnfermedad(string nombreEnfermedad)
        {
            List<Enfermedad> listaEnfermedad = new List<Enfermedad>();
            if (nombreEnfermedad == null || nombreEnfermedad.Length == 0)
            {
                listaEnfermedad = (from enfermedad in _db.Enfermedad
                                     select new Enfermedad
                                     {
                                         EnfermedadId = enfermedad.EnfermedadId,
                                         Nombre = enfermedad.Nombre,
                                         Descripcion = enfermedad.Descripcion
                                     }).ToList();

                ViewBag.Enfermedad = "";
            }
            else
            {
                listaEnfermedad = (from enfermedad in _db.Enfermedad
                                     where enfermedad.Nombre.Contains(nombreEnfermedad)
                                     select new Enfermedad
                                     {
                                         EnfermedadId = enfermedad.EnfermedadId,
                                         Nombre = enfermedad.Nombre,
                                         Descripcion = enfermedad.Descripcion
                                     }).ToList();
                ViewBag.Enfermedad = nombreEnfermedad;
            }
            lista = listaEnfermedad;
            return listaEnfermedad;
        }


        public IActionResult Index()
        {
            List<Enfermedad> listaEnfermedad = new List<Enfermedad>();
            listaEnfermedad = BuscarEnfermedad("");
            return View(listaEnfermedad);
        }
        //metodo que descarga el archivo excel
        public FileResult exportarExcel()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Nombre", "Descripcion" };
            string[] nombrePropiedades = { "Nombre", "Descripcion" };
            byte[] buffer = util.generarExcel(cabeceras, nombrePropiedades, lista);
            //content type mime xlsx google
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public FileResult exportarPDF()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Nombre", "Descripcion" };
            string[] nombrePropiedades = {  "Nombre", "Descripcion" };
            string titulo = "Reporte de Enfermedades";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }
    }
}
