using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    public class EspecialidadController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<Especialidad> listaEspecialidad = new List<Especialidad>();
        public EspecialidadController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            listaEspecialidad = (from especialidad in _db.Especialidad
                                 select new Especialidad
                                 {
                                     EspecialidadId = especialidad.EspecialidadId,
                                     Nombre = especialidad.Nombre,
                                     Descripcion = especialidad.Descripcion.Substring(0, 85) + "..."  //Cambio
                                 }).ToList();
            var model = listaEspecialidad;
            return View("Index", model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var ultimoRegistro = _db.Set<Especialidad>().OrderByDescending(e => e.EspecialidadId).FirstOrDefault();
            ViewBag.ID = ultimoRegistro.EspecialidadId + 1;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Especialidad especialidad)
        {
            string Error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(especialidad);
                }
                else
                {
                    _db.Especialidad.Add(especialidad);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        //Apartir de aca todo nuevo
        public IActionResult Details(int id)
        {
            Especialidad oEspecialidad = _db.Especialidad
                         .Where(e => e.EspecialidadId == id).First();
            return View(oEspecialidad);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Especialidad oEspecialidad = _db.Especialidad
                         .Where(e => e.EspecialidadId == id).First();
            return View(oEspecialidad);
        }

        [HttpPost]
        public IActionResult Edit(Especialidad especialidad)
        {
            string error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(especialidad);
                }
                else
                {
                    _db.Especialidad.Update(especialidad);
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
        public IActionResult Delete(int? EspecialidadId)
        {
            var Error = "";
            try
            {
                Especialidad oEspecialidad = _db.Especialidad
                             .Where(e => e.EspecialidadId == EspecialidadId).First();
                _db.Especialidad.Remove(oEspecialidad);
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
