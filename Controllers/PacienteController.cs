using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    public class PacienteController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<Paciente> listaPaciente = new List<Paciente>();
        public PacienteController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            listaPaciente = (from paciente in _db.Paciente
                             select new Paciente
                             {
                                 PacienteId = paciente.PacienteId,
                                 Nombre = paciente.Nombre,
                                 Apellidos = paciente.Apellidos,
                                 Direccion = paciente.Direccion,
                                 TelefonoContacto = paciente.TelefonoContacto,
                                 Foto = paciente.Foto
                             }).ToList();
            var model = listaPaciente;
            return View("Index", model);
        }
        public IActionResult Create()
        {
            var ultimoRegistro = _db.Set<Paciente>().OrderByDescending(e => e.PacienteId).FirstOrDefault();
            ViewBag.ID = ultimoRegistro.PacienteId + 1;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Paciente paciente)
        {
            string Error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(paciente);
                }
                else
                {
                    _db.Paciente.Add(paciente);
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
            Paciente paciente = _db.Paciente
                         .Where(e => e.PacienteId == id).First();
            return View(paciente);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Paciente paciente = _db.Paciente
                         .Where(e => e.PacienteId == id).First();
            return View(paciente);
        }

        [HttpPost]
        public IActionResult Edit(Paciente paciente)
        {
            string error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(paciente);
                }
                else
                {
                    _db.Paciente.Update(paciente);
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
        public IActionResult Delete(int? PacienteId)
        {
            var Error = "";
            try
            {
                Paciente paciente = _db.Paciente
                             .Where(e => e.PacienteId == PacienteId).First();
                _db.Paciente.Remove(paciente);
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
