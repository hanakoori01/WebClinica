using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Clinica.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClinica.Filter;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class CitasController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<CitaMedica> listaCitas = new List<CitaMedica>();
        List<Citas> lista = new List<Citas>();

        static private string _Fecha;

        public CitasController(DBClinicaAcmeContext db)
        {
            _db = db;
        }

        public List<CitaMedica> listarCitas()
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
                              Diagnostico = citas.Diagnostico,
                              FechaCita = citas.FechaCita
                          }).ToList();
            return listaCitas;
        }

        private void CargarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<Citas>().OrderByDescending(e => e.CitaId).FirstOrDefault();
            if (ultimoRegistro == null)
            {
                ViewBag.ID = 1;
            }
            else
            {
                ViewBag.ID = ultimoRegistro.CitaId + 1;
            }
        }

        private void CargarMedicos()
        {
            List<SelectListItem> listaMedicos = new List<SelectListItem>();
            listaMedicos = (from medico in _db.Medico
                            orderby medico.Nombre
                            join especialidad in _db.Especialidad
                            on medico.EspecialidadId equals especialidad.EspecialidadId
                            select new SelectListItem
                            {
                                Text = medico.Apellidos + ", "
                                     + medico.Nombre + " - "
                                     + especialidad.Nombre,
                                Value = medico.MedicoId.ToString()
                            }
                                   ).ToList();
            ViewBag.ListaMedicos = listaMedicos;
        }

        private void ListarPacientes()
        {
            List<SelectListItem> listaPacientes = new List<SelectListItem>();

            listaPacientes = (from pacientes in _db.Paciente
                              orderby pacientes.Apellidos
                              select new SelectListItem
                              {
                                  Text = pacientes.Nombre + ", "
                                       + pacientes.Apellidos,
                                  Value = pacientes.PacienteId.ToString()
                              }).ToList();
            ViewBag.ListaPacientes = listaPacientes;
        }

        private void ListarEspecialidades()
        {
            List<SelectListItem> listaEspecialidades = new List<SelectListItem>();
            listaEspecialidades = (from especialidad in _db.Especialidad
                                   orderby especialidad.Nombre
                                   select new SelectListItem
                                   {
                                       Text = especialidad.Nombre,
                                       Value = especialidad.EspecialidadId.ToString()
                                   }
                                   ).ToList();
            ViewBag.ListaEspecialidades = listaEspecialidades;
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

        public IActionResult Index()
        {
            listaCitas = listarCitas();
            return View(listaCitas);
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

        [HttpGet]
        public IActionResult Create(int PacienteId)
        {
            CargarMedicos();
            CargarUltimoRegistro();
            ListarPacientes();
            ListarEspecialidades();
            if (PacienteId != 0)
            {
                BuscarPaciente(PacienteId);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Citas citas)
        {
            string Error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(citas);
                }
                else
                {
                    CargarUltimoRegistro();
                    Citas _citas = new Citas();
                    _citas.CitaId = ViewBag.ID;
                    _citas.EspecialidadId = citas.EspecialidadId;
                    _citas.MedicoId = citas.MedicoId;
                    _citas.PacienteId = citas.PacienteId;
                    _citas.FechaCita = citas.FechaCita;
                    _citas.Diagnostico = citas.Diagnostico;
                    _db.Citas.Add(_citas);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int Id)
        {
            CargarUltimoRegistro();
            CargarMedicos();
            ListarPacientes();
            ListarEspecialidades();
            buscarCita(Id);
            int recCount = _db.Citas.Count(e => e.CitaId == Id);
            Citas _citas = (from c in _db.Citas
                            where c.CitaId == Id
                            select c).DefaultIfEmpty().Single();
            ViewBag.FechaCita = _citas.FechaCita.ToString("yyyy-MM-dd");
            _Fecha = ViewBag.FechaCita;
            return View(_citas);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            CargarUltimoRegistro();
            CargarMedicos();
            ListarPacientes();
            ListarEspecialidades();
            buscarCita(id);
            int recCount = _db.Citas.Count(e => e.CitaId == id);
            Citas _citas = (from c in _db.Citas
                            where c.CitaId == id
                            select c).DefaultIfEmpty().Single();
            ViewBag.FechaCita = _citas.FechaCita.ToString("yyyy-MM-dd");
            _Fecha = ViewBag.FechaCita;
            return View(_citas);
        }

        [HttpPost]
        public IActionResult Edit(Citas cita)
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

        [HttpPost]
        public IActionResult Delete(int CitaId)
        {
            string Error = "";
            try
            {
                Citas oCita = _db.Citas
                    .Where(c => c.CitaId == CitaId).First();
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

    }
}
