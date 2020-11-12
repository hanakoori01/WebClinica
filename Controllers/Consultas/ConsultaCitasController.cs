using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Clinica.Models.ViewModel;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Filter;
using WebClinica.Models;

namespace Clinica.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class ConsultaCitasController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<CitaMedica> listaCitas = new List<CitaMedica>();
        static List<CitaMedica> lista = new List<CitaMedica>();

        public ConsultaCitasController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index(DateTime FechaInicio, DateTime FechaFinal)
        {
           String _FechaInicio =  FechaInicio.ToString("dd-MM-yyyy");
           String _FechaFinal = FechaFinal.ToString("dd-MM-yyyy");
            if ((_FechaInicio == "01-01-0001") || (_FechaFinal == "01-01-0001"))
            {
                listaCitas = (from citas in _db.Citas

                              join pacientes in _db.Paciente
                              on citas.PacienteId equals
                              pacientes.PacienteId

                              join medico in _db.Medico
                              on citas.MedicoId equals
                              medico.MedicoId

                              join especialidad in _db.Especialidad
                              on citas.EspecialidadId equals
                              especialidad.EspecialidadId
                              select new CitaMedica
                              {
                                  CitaId = citas.CitaId,
                                  NombrePaciente = pacientes.Nombre +
                                              " " + pacientes.Apellidos,
                                  MedicoId = medico.MedicoId,
                                  NombreMedico = medico.Nombre + " " + medico.Apellidos,
                                  NombreEspecialidad = especialidad.Nombre,
                                  FechaCita = citas.FechaCita,
                                  Diagnostico = citas.Diagnostico
                              }).ToList();
                ViewBag.Especialidad = "";
            }
            else
            {
                listaCitas = (from citas in _db.Citas

                              join pacientes in _db.Paciente
                              on citas.PacienteId equals
                              pacientes.PacienteId

                              join medico in _db.Medico
                              on citas.MedicoId equals
                              medico.MedicoId

                              join especialidad in _db.Especialidad
                              on citas.EspecialidadId equals
                              especialidad.EspecialidadId

                              where (citas.FechaCita >= FechaInicio 
                                  && citas.FechaCita <= FechaFinal)

                              select new CitaMedica
                              {
                                  CitaId = citas.CitaId,
                                  NombrePaciente = pacientes.Nombre +
                                              " " + pacientes.Apellidos,
                                  MedicoId = medico.MedicoId,
                                  NombreMedico = medico.Nombre + " " + medico.Apellidos,
                                  NombreEspecialidad = especialidad.Nombre,
                                  FechaCita = citas.FechaCita,
                                  Diagnostico = citas.Diagnostico
                              }).ToList();
                ViewBag.fechaInicio = FechaInicio.ToString("yyyy-MM-dd");
                ViewBag.fechaFinal = FechaFinal.ToString("yyyy-MM-dd");
            }
            lista = listaCitas;
            return View(listaCitas);
        }
        //metodo que descarga el archivo excel
        public FileResult exportarExcel()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Cita ID", "Paciente", "Medico","Especialidad", "Fecha Cita", "Diagnostico" };
            string[] nombrePropiedades = { "CitaId", "NombrePaciente", "NombreMedico",
                                           "NombreEspecialidad","FechaCita", "Diagnostico"};
            byte[] buffer = util.generarExcel(cabeceras, nombrePropiedades, lista);
            //content type mime xlsx google
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public FileResult exportarPDF()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Cita ID", "Paciente", "Medico", "Especialidad", "Fecha Cita", "Diagnostico" };

            string[] nombrePropiedades = { "CitaId", "NombrePaciente", "NombreMedico",
                                           "NombreEspecialidad","FechaCita", "Diagnostico"};
            string titulo = "Reporte de Citas Médicas";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }
    }
}
