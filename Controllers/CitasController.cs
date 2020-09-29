using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    public class CitasController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<Citas> listaCitas = new List<Citas>();
        public CitasController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            listaCitas = (from citas in _db.Citas
                                 select new Citas
                                 {
                                     CitaId = citas.CitaId,
                                     PacienteId = citas.PacienteId,
                                     MedicoId = citas.MedicoId,
                                     Diagnostico = citas.Diagnostico.Substring(0, 85) + "..."  //Cambio
                                 }).ToList();
            var model = listaCitas;
            return View("Index", model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var ultimoRegistro = _db.Set<Citas>().OrderByDescending(e => e.CitaId).FirstOrDefault();
            ViewBag.ID = ultimoRegistro.CitaId + 1;
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
                    _db.Citas.Add(citas);
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
            Citas oCitas = _db.Citas
                         .Where(e => e.CitaId == id).First();
            return View(oCitas);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Citas oCitas = _db.Citas
                         .Where(e => e.CitaId == id).First();
            return View(oCitas);
        }

        [HttpPost]
        public IActionResult Edit(Citas citas)
        {
            string error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(citas);
                }
                else
                {
                    _db.Citas.Update(citas);
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
        public IActionResult Delete(int? CitaId)
        {
            var Error = "";
            try
            {
                Citas citas = _db.Citas
                             .Where(e => e.CitaId == CitaId).First();
                _db.Citas.Remove(citas);
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
