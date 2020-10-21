using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Clinica.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    public class CitasController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<CitaMedica> listaCitas = new List<CitaMedica>();

        static private string _Fecha;

        public CitasController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index()
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
                              FechaCita = citas.FechaCita
                          }).ToList();
            ViewBag.Controlador = "Citas";
            ViewBag.Accion = "Index";
            return View(listaCitas);
        }

        private void determinarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<Citas>().OrderByDescending(
                e => e.CitaId).FirstOrDefault();
            ViewBag.ID = ultimoRegistro.CitaId + 1;
        }

        private void cargarMedicos()
        {
            List<SelectListItem> listaMedicos = new List<SelectListItem>();
            listaMedicos = (from medico in _db.Medico
                            orderby medico.Nombre
                            join especialidad in _db.Especialidad
                            on medico.EspecialidadId equals especialidad.EspecialidadId
                            select new SelectListItem
                            {
                                Text = medico.Apellidos + ", " + medico.Nombre
                                            + " - " + especialidad.Nombre + ", " +
                                            medico.MedicoId
                            }
                                   ).ToList();
            ViewBag.ListaMedicos = listaMedicos;
        }

        public IActionResult Create(int PacienteId)
        {
            cargarMedicos();
            determinarUltimoRegistro();
            if (PacienteId != 0)
            {
                BuscarPaciente(PacienteId);
            }
            ViewBag.Controlador = "Citas";
            ViewBag.Accion = "Create";
            return View();
        }
        private void BuscarPaciente(int PacienteId)
        {
            Paciente oPaciente = _db.Paciente
           .Where(p => p.PacienteId == PacienteId).FirstOrDefault();
            if (oPaciente != null)
            {
                ViewBag.PacienteID = oPaciente.PacienteId;
                ViewBag.Nombre = oPaciente.Nombre + " " + oPaciente.Apellidos;
                @ViewBag.FechaCita = _Fecha;
            }
            else
            {
                ViewBag.Error = "Paciente no registrado, intente de nuevo!";
            }
        }

        public IActionResult ListarPacientes()
        {
            List<Paciente> listaPacientes = new List<Paciente>();

            listaPacientes = (from pacientes in _db.Paciente
                              select new Paciente
                              {
                                  PacienteId = pacientes.PacienteId,
                                  Nombre = pacientes.Nombre,
                                  Apellidos = pacientes.Apellidos,
                                  Direccion = pacientes.Direccion
                              }).ToList();
            return View(listaPacientes);
        }

        private int buscarEspecialidad(int medicoId)
        {
            int especialidadID = 0;
            Medico oMedico = _db.Medico
                .Where(m => m.MedicoId == medicoId).SingleOrDefault();
            if (oMedico != null)
            {
                especialidadID = oMedico.EspecialidadId;
            }
            return especialidadID;
        }

        private void buscarCita(int Id)
        {
            listaCitas = (from _citas in _db.Citas
                          join _medicos in _db.Medico on _citas.MedicoId equals _medicos.MedicoId
                          join _especialidad in _db.Especialidad on _citas.EspecialidadId
                                                              equals _especialidad.EspecialidadId
                          join _pacientes in _db.Paciente on _citas.PacienteId equals _pacientes.PacienteId
                          where _citas.CitaId == Id
                          select new CitaMedica
                          {
                              CitaId = _citas.CitaId,
                              FechaCita = _citas.FechaCita,
                              PacienteId = _citas.PacienteId,
                              Diagnostico = _citas.Diagnostico,
                              MedicoId = _citas.MedicoId,
                              NombrePaciente = _pacientes.Nombre + " " + _pacientes.Apellidos,
                              NombreMedico = _medicos.Nombre + " " + _medicos.Apellidos,
                              NombreEspecialidad = _especialidad.Nombre,
                          }).ToList();
        }

        //Apartir de aca todo nuevo
        public IActionResult Details(int id)
        {
            Citas oCitas = _db.Citas
                         .Where(e => e.CitaId == id).First();
            return View(oCitas);
        }

        public IActionResult Delete(int Id)
        {
            buscarCita(Id);
            return View(listaCitas);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Deleted(int Id)
        {
            string Error = "";
            try
            {
                Citas oCita = _db.Citas
                    .Where(c => c.CitaId == Id).First();
                if (oCita != null)
                {
                    _db.Citas.Remove(oCita);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Created(CitaMedica cita)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    cargarMedicos();
                }
                else
                {
                    var citaId = buscarEspecialidad(cita.MedicoId);
                    Citas _cita = new Citas();
                    //_cita.CitaId = cita.CitaId;
                    _cita.PacienteId = cita.PacienteId;
                    _cita.MedicoId = cita.MedicoId;
                    _cita.FechaCita = cita.FechaCita;
                    _cita.Diagnostico = cita.Diagnostico;
                    _cita.EspecialidadId = citaId;
                    _db.Citas.Add(_cita);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            //Si se quiere caer de nuevo en Create
            //para seguir agregando citas
            cargarMedicos();
            determinarUltimoRegistro();
            return View("Create");

        }

        public IActionResult Edit(int Id)
        {
            buscarCita(Id);
            CitaMedica _citaMedica = new CitaMedica();
            foreach (CitaMedica item in listaCitas)
            {
                _citaMedica.CitaId = item.CitaId;
                _citaMedica.FechaCita = item.FechaCita.Date;
                _citaMedica.PacienteId = item.PacienteId;
                _citaMedica.NombrePaciente = item.NombrePaciente;
                _citaMedica.MedicoId = item.MedicoId;
                _citaMedica.NombreMedico = item.NombrePaciente;
                _citaMedica.EspecialidadId = item.EspecialidadId;
                _citaMedica.NombreEspecialidad = item.NombreEspecialidad;
                _citaMedica.Diagnostico = item.Diagnostico;
            }
            ViewBag.Medico = _citaMedica.MedicoId;
            ViewBag.FechaCita = _citaMedica.FechaCita.ToString("yyyy-MM-dd");
            _Fecha = ViewBag.FechaCita;
            cargarMedicos();
            return View(_citaMedica);
        }
        [HttpPost]
        public IActionResult Edit(CitaMedica cita)
        {
            string error;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(cita);
                }
                else
                {
                    Citas _cita = new Citas();
                    _cita.CitaId = cita.CitaId;
                    _cita.PacienteId = cita.PacienteId;
                    _cita.MedicoId = cita.MedicoId;
                    _cita.FechaCita = cita.FechaCita;
                    _cita.Diagnostico = cita.Diagnostico;
                    _cita.EspecialidadId = buscarEspecialidad(cita.MedicoId);
                    _db.Citas.Update(_cita);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
