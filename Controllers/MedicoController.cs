using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebClinica.Models;

namespace WebClinica.Controllers
{
    public class MedicoController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<Medico> listaMedico = new List<Medico>();
        public MedicoController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            listaMedico = (from medico in _db.Medico
                           select new Medico
                           {
                               MedicoId = medico.MedicoId,
                               Nombre = medico.Nombre,
                               Apellidos = medico.Apellidos,
                               Direccion = medico.Direccion,
                               TelefonoFijo = medico.TelefonoFijo,
                               TelefonoCelular = medico.TelefonoCelular,
                               Foto = medico.Foto,
                               EspecialidadId = medico.EspecialidadId
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
    }
}
