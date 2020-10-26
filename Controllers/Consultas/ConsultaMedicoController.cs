using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    public class ConsultaMedicoController : Controller
    {
        static List<Medico> lista = new List<Medico>();

        private readonly DBClinicaAcmeContext _db;


        public ConsultaMedicoController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public List<Medico> BuscarMedico(string nombreMedico)
        {
            List<Medico> listaMedico = new List<Medico>();
            if (nombreMedico == null || nombreMedico.Length == 0)
            {
                listaMedico = (from medico in _db.Medico
                               select new Medico
                               {
                                   MedicoId = medico.MedicoId,
                                   Nombre = medico.Nombre,
                                   Apellidos = medico.Apellidos,
                                   Direccion = medico.Direccion,
                                   TelefonoFijo = medico.TelefonoFijo,
                                   TelefonoCelular = medico.TelefonoCelular,
                                   EspecialidadId = medico.EspecialidadId
                               }).ToList();



                ViewBag.Medico = "";
            }
            else
            {
                listaMedico = (from medico in _db.Medico
                               where medico.Nombre.Contains(nombreMedico)
                               select new Medico
                               {
                                   MedicoId = medico.MedicoId,
                                   Nombre = medico.Nombre,
                                   Apellidos = medico.Apellidos,
                                   Direccion = medico.Direccion,
                                   TelefonoFijo = medico.TelefonoFijo,
                                   TelefonoCelular = medico.TelefonoCelular,
                                   EspecialidadId = medico.EspecialidadId
                               }).ToList();
                ViewBag.Medico = nombreMedico;
            }
            lista = listaMedico;
            return listaMedico;
        }




        public IActionResult Index()
        {
            List<Medico> listaMedico = new List<Medico>();
            listaMedico = BuscarMedico("");
            return View(listaMedico);
        }
        //metodo que descarga el archivo excel
        public FileResult exportarExcel()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "MedicoId", "Nombre", "Apellidos", "Direccion", "Telefono Fijo", "Telefono Celular", "Foto", "Especialidad Id" };
            string[] nombrePropiedades = { "MedicoId", "Nombre", "Apellidos", "Direccion", "Telefono Fijo", "Telefono Celular", "Foto", "Especialidad Id" };
            byte[] buffer = util.generarExcel(cabeceras, nombrePropiedades, lista);
            //content type mime xlsx google
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public FileResult exportarPDF()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "MedicoId", "Nombre", "Apellidos", "Direccion", "Telefono Fijo", "Telefono Celular", "Foto", "Especialidad Id" };
            string[] nombrePropiedades = { "MedicoId", "Nombre", "Apellidos", "Direccion", "Telefono Fijo", "Telefono Celular", "Foto", "Especialidad Id" };
            string titulo = "Reporte de Medico";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }
    }
}
