using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Models;

namespace Clinica.Controllers
{
    public class TipoUsuariosController : Controller
    {
        public List<TipoUsuario> listaTipoUsuarios;
        private readonly DBClinicaAcmeContext _db;
    
        public TipoUsuariosController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public List<TipoUsuario> listarTipoUsuarios()
        {
            listaTipoUsuarios = (from tipoUsuario in _db.TipoUsuario
                             select new TipoUsuario
                             {
                                 TipoUsuarioId = tipoUsuario.TipoUsuarioId,
                                 Nombre = tipoUsuario.Nombre
                             }).ToList();
            return listaTipoUsuarios;
        }
        private void determinarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<TipoUsuario>().OrderByDescending(
                t => t.TipoUsuarioId).FirstOrDefault();
            if (ultimoRegistro != null)
            {
                ViewBag.ID = ultimoRegistro.TipoUsuarioId + 1;
            }
            else
            {
                ViewBag.ID = 1;
            }
        }
        public IActionResult Index()
        {
            List<TipoUsuario> listaUsuarios = new List<TipoUsuario>();
            determinarUltimoRegistro();
            listaUsuarios = listarTipoUsuarios();
            return View(listaUsuarios);
        }

        public string Create(TipoUsuario _TipoUsuario)
        {
            string rpta = "";
            try
            {
                if (!ModelState.IsValid && _TipoUsuario == null)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class='list-group'>";
                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item list-group-item-danger'>";
                        rpta += item;
                        rpta += "</li>";
                    }
                    rpta += "</ul>";
                }
                else
                {
                    rpta = "OK";
                    TipoUsuario tipoUsuario = new TipoUsuario();
                    tipoUsuario.Nombre = _TipoUsuario.Nombre;
                    _db.TipoUsuario.Add(tipoUsuario);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
            }
        public JsonResult Edit(int? id)
        {
            TipoUsuario _TipoUsuario = (from t in _db.TipoUsuario
                                        where t.TipoUsuarioId == id
                                        select t).DefaultIfEmpty().First();
            return Json(_TipoUsuario);
        }
        public JsonResult Details(int? id)
        {
            TipoUsuario _tipousuario = (from t in _db.TipoUsuario
                                where t.TipoUsuarioId == id
                                select t).DefaultIfEmpty().Single();
            return Json(_tipousuario);
        }
    }
}
