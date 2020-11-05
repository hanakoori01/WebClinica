using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    public class EnfermedadController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<Enfermedad> listaEnfermedad = new List<Enfermedad>();
       

        public EnfermedadController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            listaEnfermedad = (from enfermedad in _db.Enfermedad
                               select new Enfermedad
                               {
                                   EnfermedadId = enfermedad.EnfermedadId,
                                     Nombre = enfermedad.Nombre,
                                     Descripcion = enfermedad.Descripcion.Substring(0, 85) + "..." 
                                 }).ToList();
            var model = listaEnfermedad;
            return View("Index", model);
        }

        private void cargarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<Enfermedad>().OrderByDescending(e => e.EnfermedadId).FirstOrDefault();
            if (ultimoRegistro == null)
            {
                ViewBag.ID = 1;
            }
            else
            {
                ViewBag.ID = ultimoRegistro.EnfermedadId + 1;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            cargarUltimoRegistro();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Enfermedad enfermedad)
        {
            string Error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(enfermedad);
                }
                else
                {
                    _db.Enfermedad.Add(enfermedad);
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
            Enfermedad oEnfermedad = _db.Enfermedad
                         .Where(e => e.EnfermedadId == id).First();
            return View(oEnfermedad);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Enfermedad oEnfermedad = _db.Enfermedad
                        .Where(e => e.EnfermedadId == id).First();
            return View(oEnfermedad);
        }

        [HttpPost]
        public IActionResult Edit(Enfermedad enfermedad)
        {
            string error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(enfermedad);
                }
                else
                {
                    _db.Enfermedad.Update(enfermedad);
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
        public IActionResult Delete(int? EnfermedadId)
        {
            var Error = "";
            try
            {
                Enfermedad oEnfermedad = _db.Enfermedad
                             .Where(e => e.EnfermedadId == EnfermedadId).First();
                _db.Enfermedad.Remove(oEnfermedad);
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
