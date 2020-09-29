using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebClinica.Models;
using WebClinica.Models.ViewModel;
using System.Threading.Tasks;

namespace WebClinica.Controllers
{
    public class MedicoController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<MedicoEspecialidad> listaMedico = new List<MedicoEspecialidad>();
        public MedicoController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            listaMedico = (from medico in _db.Medico
                           join especialidad in _db.Especialidad
                           on medico.EspecialidadId equals especialidad.EspecialidadId
                           select new MedicoEspecialidad
                           {
                               MedicoId = medico.MedicoId,
                               Nombre = medico.Nombre,
                               Apellidos = medico.Apellidos,
                               Direccion = medico.Direccion,
                               TelefonoFijo = medico.TelefonoFijo,
                               TelefonoCelular = medico.TelefonoCelular,
                               Especialidad = especialidad.Nombre
                           }).ToList();
            var model = listaMedico;
            return View("Index", model);
        }
        public IActionResult Create()
        {
            var ultimoRegistro = _db.Set<Medico>().OrderByDescending(e => e.MedicoId).FirstOrDefault();
            ViewBag.ID = ultimoRegistro.MedicoId + 1;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Medico medico)
        {
            string Error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(medico);
                }
                else
                {
                    _db.Medico.Add(medico);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            Medico medico = _db.Medico
                         .Where(e => e.MedicoId == id).First();
            return View(medico);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Medico medico = _db.Medico
                         .Where(e => e.MedicoId == id).First();
            return View(medico);
        }

        [HttpPost]
        public IActionResult Edit(Medico medico)
        {
            string error = "";
            try
            {
                if (!ModelState.IsValid)
                {
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
        public IActionResult Delete(int? MedicoId)
        {
            var Error = "";
            try
            {
                Medico medico = _db.Medico
                             .Where(e => e.MedicoId == MedicoId).First();
                _db.Medico.Remove(medico);
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
