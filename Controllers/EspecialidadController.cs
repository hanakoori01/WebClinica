using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        static List<Especialidad> lista = new List<Especialidad>();
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
                                     Descripcion = especialidad.Descripcion.Substring(0, 85) + "..."  
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
      
        public FileResult exportarExcel()
        {
            var lista = _db.Set<Especialidad>().ToList();
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Especialidad", "Nombre", "Descripcion" };
            string[] nombrePropiedades = { "EspecialidadId", "Nombre", "Descripcion" };
            byte[] buffer = util.generarExcel(cabeceras, nombrePropiedades, lista);
            //content type mime xlsx google
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public FileResult exportarPDF()
        {
            var lista = _db.Set<Especialidad>().ToList();
            Utilitarios util = new Utilitarios();
            string[] nombrePropiedades = { "EspecialidadId", "Nombre", "Descripcion" };
            string titulo = "Reporte de Especialidades";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }
    }
}
