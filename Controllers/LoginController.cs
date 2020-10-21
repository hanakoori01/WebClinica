using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Models;

namespace Clinica.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly DBClinicaAcmeContext _db;
  
        public LoginController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public string _Login(string user, string pass)
        {
            string rpta = "";
            string claveCifrada = Utilitarios.CifrarDatos(pass);
            int nVeces = _db.Usuario.Where(u => u.Nombre == user
            && u.Password == claveCifrada).Count();
            if (nVeces !=0)
            {
                rpta = "OK";
                Usuario User = _db.Usuario.Where(u => u.Nombre == user
                        && u.Password == claveCifrada).First();
                //HttpContext.Session.SetString("Usuario", User.UsuarioId.ToString());
                //int idTipo = User.TipoUsuarioId;
            }
            return rpta;
        }
    }
}
