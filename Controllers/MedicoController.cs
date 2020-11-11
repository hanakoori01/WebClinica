using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebClinica.Models;
using WebClinica.Models.ViewModel;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClinica.Filter;

namespace WebClinica.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class MedicoController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<MedicoEspecialidad> listaMedico = new List<MedicoEspecialidad>();
        static List<MedicoEspecialidad> lista = new List<MedicoEspecialidad>();
        public MedicoController(DBClinicaAcmeContext db)
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
        private void cargarEspecialidades()
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

        public IActionResult Index()
        {
            listaMedico = BuscarMedicoEspecialidad("");
            return View(listaMedico);
        }

        [HttpGet]
        public IActionResult Create()
        {
            cargarEspecialidades();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Medico medico)
        {
            int nVeces = 0;

            try
            {
                nVeces = _db.Medico.Where(m => m.MedicoId == medico.MedicoId).Count();
                if (!ModelState.IsValid || nVeces >= 1)
                {
                    if (nVeces >= 1) ViewBag.Error = "Este id de médico ya existe!";
                    cargarEspecialidades();
                    return View(medico);
                }
                else
                {
                    Medico _medico = new Medico
                    {
                        MedicoId = medico.MedicoId,
                        Nombre = medico.Nombre,
                        Apellidos = medico.Apellidos,
                        Direccion = medico.Direccion,
                        TelefonoFijo = medico.TelefonoFijo,
                        TelefonoCelular = medico.TelefonoCelular,
                        EspecialidadId = medico.EspecialidadId
                    };

                    _db.Medico.Add(_medico);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            cargarEspecialidades();
            Medico oMedico = _db.Medico
                 .Where(m => m.MedicoId == id).First();
            return View(oMedico);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            cargarEspecialidades();
            int recCount = _db.Medico.Count(e => e.MedicoId == id);
            Medico _medico = (from p in _db.Medico
                              where p.MedicoId == id
                              select p).DefaultIfEmpty().Single();
            return View(_medico);
        }

        [HttpPost]
        public IActionResult Edit(Medico medico)
        {
            string error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    cargarEspecialidades();
                    return View(medico);
                }
                else
                {
                    _db.Medico.Update(medico);
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
        public IActionResult Delete(int MedicoId)
        {
            var Error = "";
            try
            {
                Medico oMedico = _db.Medico
                             .Where(e => e.MedicoId == MedicoId).First();
                _db.Medico.Remove(oMedico);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

      
    }
}
