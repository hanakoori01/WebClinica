using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Models;
using WebClinica.Models.ViewModel;

namespace WebClinica.Controllers
{
    public class ConsultaMedicoController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<MedicoEspecialidad> listaMedico = new List<MedicoEspecialidad>();
        static List<MedicoEspecialidad> lista = new List<MedicoEspecialidad>();

        public ConsultaMedicoController(DBClinicaAcmeContext db)
        {
            _db = db;
        }

        public List<MedicoEspecialidad> BuscarMedicoEspecialidad(string nombreEspecialidad)
        {
            List<MedicoEspecialidad> listaMedicoEspecialidad = new List<MedicoEspecialidad>();
            if (nombreEspecialidad == null || nombreEspecialidad.Length == 0)
            {
                listaMedico = (from medico in _db.Medico
                               join especialidad in _db.Especialidad
                               on medico.EspecialidadId equals especialidad.EspecialidadId
                               select new MedicoEspecialidad
                               {
                                   MedicoId = medico.MedicoId,
                                   Nombre = medico.Nombre,
                                   Apellidos = medico.Apellidos,
                                   Direccion = medico.Direccion.Length > 50 ?
                                               medico.Direccion.Substring(0, 50)
                                               + "..." : medico.Direccion,
                                   TelefonoFijo = medico.TelefonoFijo,
                                   TelefonoCelular = medico.TelefonoCelular,
                                   Especialidad = especialidad.Nombre
                               }).ToList();
                ViewBag.Especialidad = "";
            }
            else
            {
                listaMedico = (from medico in _db.Medico
                               join especialidad in _db.Especialidad
                               on medico.EspecialidadId equals especialidad.EspecialidadId
                               where especialidad.Nombre.Contains(nombreEspecialidad)
                               select new MedicoEspecialidad
                               {
                                   MedicoId = medico.MedicoId,
                                   Nombre = medico.Nombre,
                                   Apellidos = medico.Apellidos,
                                   Direccion = medico.Direccion.Length > 50 ?
                                               medico.Direccion.Substring(0, 50)
                                               + "..." : medico.Direccion,
                                   TelefonoFijo = medico.TelefonoFijo,
                                   TelefonoCelular = medico.TelefonoCelular,
                                   Especialidad = especialidad.Nombre
                               }).ToList();
                ViewBag.Especialidad = nombreEspecialidad;
            }
            lista = listaMedico;
            return listaMedico;
        }




        public IActionResult Index()
        {
            listaMedico = BuscarMedicoEspecialidad("");
            return View(listaMedico);
        }
        //metodo que descarga el archivo excel
        public FileResult exportarExcel()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Nombre", "Apellidos", "Direccion", "Telefono Fijo", "Telefono Celular", "Especialidad Nombre" };
            string[] nombrePropiedades = { "Nombre", "Apellidos", "Direccion", "TelefonoFijo", "TelefonoCelular", "Especialidad" };
            byte[] buffer = util.generarExcel(cabeceras, nombrePropiedades, lista);
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public FileResult exportarPDF()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Nombre", "Apellidos", "Direccion", "Telefono Fijo", "Telefono Celular", "Especialidad Nombre" };
            string[] nombrePropiedades = { "Nombre", "Apellidos", "Direccion", "TelefonoFijo", "TelefonoCelular", "Especialidad" };
            string titulo = "Reporte de Medico";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }
    }
}
