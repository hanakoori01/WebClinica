using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Clinica.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClinica.Models;

namespace Clinica.Controllers
{
    public class UsuariosController : Controller
    {
        public List<UsuarioTipoUsuario> listaUsuarios;
        private readonly DBClinicaAcmeContext _db;
        public UsuariosController(DBClinicaAcmeContext db)
        {
            _db = db;
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            var Error = "";
            try
            {
                Usuario _Usuario = _db.Usuario
                   .Where(u => u.UsuarioId == id).First();
                _db.Usuario.Remove(_Usuario);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }



        public List<UsuarioTipoUsuario> listarUsuarios()
        {
            listaUsuarios = (from usuario in _db.Usuario
                             join _TipoUsuario in _db.TipoUsuario
                             on usuario.TipoUsuarioId equals _TipoUsuario.TipoUsuarioId
                             select new UsuarioTipoUsuario
                             {
                                 UsuarioId = usuario.UsuarioId,
                                 Nombre = usuario.Nombre,
                                 TipoUsuarioNombre = _TipoUsuario.Nombre,
                                 Password = usuario.Password,
                             }).ToList();
            return listaUsuarios;
        }

        public void llenarTipoUsuario()
        {

            List<SelectListItem> listaTipoUsuario = new List<SelectListItem>();

                listaTipoUsuario = (from tipousuario in _db.TipoUsuario
                                    select new SelectListItem
                                    {
                                        Text = tipousuario.Nombre,
                                        Value = tipousuario.TipoUsuarioId.ToString()
                                    }).ToList();
                //listaTipoUsuario.Insert(0, new SelectListItem
                //{
                //    Text = "--Seleccione--",
                //    Value = ""
                //});
            ViewBag.listaTipoUsuario = listaTipoUsuario;
        }
        public Usuario recuperarUsuario(int id)
        {
            Usuario _Usuario = new Usuario();
           _Usuario = (from usuario in _db.Usuario
                               where usuario.UsuarioId == id
                               select new Usuario
                               {
                                   UsuarioId = usuario.UsuarioId,
                                   Nombre = usuario.Nombre,
                                   TipoUsuarioId = usuario.TipoUsuarioId
                               }).First();
            
            return _Usuario;
        }
        private void cargarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<Usuario>().OrderByDescending(
                e => e.UsuarioId).FirstOrDefault();
            if (ultimoRegistro != null)
            {
                ViewBag.ID = ultimoRegistro.UsuarioId + 1;
            }
            else
            {
                ViewBag.ID = 1;
            }
        }
     public IActionResult Index()
        {
            List<UsuarioTipoUsuario> listaUsuarios = new List<UsuarioTipoUsuario>();
            cargarUltimoRegistro();
            llenarTipoUsuario();
            listaUsuarios = listarUsuarios();
            return View(listaUsuarios);
        }
        public string Create(Usuario _Usuario)
        {
            string rpta = "";
            try
            {
                if (!ModelState.IsValid && _Usuario == null)
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
                    string pass = Utilitarios.CifrarDatos(_Usuario.Password);
                    Usuario user = new Usuario();
                    user.Nombre = _Usuario.Nombre;
                    user.TipoUsuarioId = _Usuario.TipoUsuarioId;
                    user.Password = pass;
                    _db.Usuario.Add(user);
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
            Usuario _usuario = (from u in _db.Usuario
                                  where u.UsuarioId == id
                                  select u).DefaultIfEmpty().Single();
            _usuario.Password = Utilitarios.DescifrarDatos(_usuario.Password);
            return Json(_usuario);
        }

        public string Edited(Usuario _Usuario)
        {
            string rpta = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    //Escribimos nuestra logica
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
                    string pass = Utilitarios.CifrarDatos(_Usuario.Password);
                    Usuario user = new Usuario();
                    user.Nombre = _Usuario.Nombre;
                    user.TipoUsuarioId = _Usuario.TipoUsuarioId;
                    user.Password = pass;
                    _db.Usuario.Update(user);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }

        public JsonResult Details(int? id)
        {
            Usuario _usuario = (from u in _db.Usuario
                                where u.UsuarioId == id
                                select u).DefaultIfEmpty().Single();
            string pass = Utilitarios.DescifrarDatos(_usuario.Password)
                               + " (" + "Cifrado : " + _usuario.Password + ")";
            _usuario.Password = pass;
            return Json(_usuario);
        }

        public IActionResult Delete(int? UsuarioId)
        {
            var Error = "";
            try
            {
                Usuario _Usuario = _db.Usuario
                   .Where(u => u.UsuarioId == UsuarioId).First();
                _db.Usuario.Remove(_Usuario);
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

