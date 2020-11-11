using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Filter;
using WebClinica.Models;

namespace Clinica.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class ConsultaPacientesController : Controller
    {
        private readonly DBClinicaAcmeContext _db;

        static List<Paciente> lista = new List<Paciente>();

        public ConsultaPacientesController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public List<Paciente> BuscarPaciente(string nombrePaciente)
        {
            List<Paciente> listaPaciente = new List<Paciente>();
            if (nombrePaciente == null || nombrePaciente.Length == 0)
            {
                listaPaciente = (from paciente in _db.Paciente
                                     select new Paciente
                                     {
                                         PacienteId = paciente.PacienteId,
                                         Nombre = paciente.Nombre,
                                         Apellidos = paciente.Apellidos,
                                         Direccion = paciente.Direccion,
                                         TelefonoContacto = paciente.TelefonoContacto
                                     }).ToList();
                ViewBag.Paciente = "";
            }
            else
            {
                listaPaciente = (from paciente in _db.Paciente
                                 where paciente.Nombre.Contains(nombrePaciente)
                                 select new Paciente
                                 {
                                     PacienteId = paciente.PacienteId,
                                     Nombre = paciente.Nombre,
                                     Apellidos = paciente.Apellidos,
                                     Direccion = paciente.Direccion,
                                     TelefonoContacto = paciente.TelefonoContacto
                                 }).ToList();
                ViewBag.Paciente = nombrePaciente;
            }
            lista = listaPaciente;
            return listaPaciente;
        }
        public IActionResult Index()
        {
            List<Paciente> listaPaciente = new List<Paciente>();
            listaPaciente = BuscarPaciente("");
            return View(listaPaciente);
        }
        //metodo que descarga el archivo excel
        public FileResult exportarExcel()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Paciente ID", "Nombre", "Apellidos", "Direccion","Telefono" };
            string[] nombrePropiedades = { "PacienteId", "Nombre","Apellidos", "Direccion","TelefonoContacto" };
            byte[] buffer = util.generarExcel(cabeceras, nombrePropiedades, lista);
            //content type mime xlsx google
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public FileResult exportarPDF()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Paciente ID", "Nombre", "Apellidos", "Direccion", "Telefono" };
            string[] nombrePropiedades = { "PacienteId", "Nombre", "Apellidos", "Direccion", "TelefonoContacto" };
            string titulo = "Reporte de Pacientes";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }
    }
}
