using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebClinica.Models;
using WebClinica.Models.ViewModel;

namespace WebClinica.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<UsuarioTipoUsuario> listaUsuario = new List<UsuarioTipoUsuario>();
        List<Usuario> lista = new List<Usuario>();
        public UsuarioController(DBClinicaAcmeContext db)
        {
            _db = db;
        }

        public List<UsuarioTipoUsuario> listarUsuarios()
        {
            listaUsuario = (from usuario in _db.Usuario
                            join _TipoUsuario in _db.TipoUsuario
                            on usuario.TipoUsuarioId equals _TipoUsuario.TipoUsuarioId
                            select new UsuarioTipoUsuario
                            {
                                UsuarioId = usuario.UsuarioId,
                                Nombre = usuario.Nombre,
                                TipoUsuarioNombre = _TipoUsuario.Nombre,
                                Password = usuario.Password,
                            }).ToList();
            return listaUsuario;
        }

        private void cargarUltimoRegistro()
        {
            var ultimoRegistro = _db.Set<Usuario>().OrderByDescending(e => e.UsuarioId).FirstOrDefault();
            if (ultimoRegistro == null)
            {
                ViewBag.ID = 1;
            }
            else
            {
                ViewBag.ID = ultimoRegistro.UsuarioId + 1;
            }
        }

        private void cargarTipoUsuarios()
        {
            List<SelectListItem> listaTipoUsuario = new List<SelectListItem>();
            listaTipoUsuario = (from tipoUsuario in _db.TipoUsuario
                                orderby tipoUsuario.Nombre
                                select new SelectListItem
                                {
                                    Text = tipoUsuario.Nombre,
                                    Value = tipoUsuario.TipoUsuarioId.ToString()
                                }).ToList();
            ViewBag.ListaTipoUsuario = listaTipoUsuario;
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

        public IActionResult Index()
        {
            listaUsuario = listarUsuarios();
            return View(listaUsuario);
        }



        [HttpGet]
        public IActionResult Create()
        {
            cargarTipoUsuarios();
            cargarUltimoRegistro();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            string Error = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(usuario);
                }
                else
                {
                    string password = Utilitarios.CifrarDatos(usuario.Password);
                    Usuario _usuario = new Usuario();
                    _usuario.UsuarioId = usuario.UsuarioId;
                    _usuario.TipoUsuarioId = usuario.TipoUsuarioId;
                    _usuario.Nombre = usuario.Nombre;
                    _usuario.Password = password;
                    _db.Usuario.Add(_usuario);
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
            cargarTipoUsuarios();
            int recCount = _db.Usuario.Count(e => e.UsuarioId == id);
            Usuario _usuario = (from u in _db.Usuario
                                where u.UsuarioId == id
                                select u).DefaultIfEmpty().Single();
            _usuario.Password = Utilitarios.DescifrarDatos(_usuario.Password);
            return View(_usuario);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            cargarTipoUsuarios();
            int recCount = _db.Usuario.Count(e => e.UsuarioId == id);
            Usuario _usuario = (from u in _db.Usuario
                                where u.UsuarioId == id
                                select u).DefaultIfEmpty().Single();
            _usuario.Password = Utilitarios.DescifrarDatos(_usuario.Password);
            return View(_usuario);
        }

        [HttpPost]
        public IActionResult Edit(Usuario _Usuario)
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
                    user.UsuarioId = _Usuario.UsuarioId;
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
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int UsuarioId)
        {
            var Error = "";
            try
            {
                Usuario usuario = _db.Usuario
                             .Where(e => e.UsuarioId == UsuarioId).First();
                _db.Usuario.Remove(usuario);
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
