using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Clinica.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Models;
using WebClinica.Models.ViewModel;

namespace WebClinica.Controllers.Consultas
{
    public class ConsultaUsuarioController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<UsuarioTipoUsuario> listaUsuario = new List<UsuarioTipoUsuario>();
        static List<UsuarioTipoUsuario> lista = new List<UsuarioTipoUsuario>();

        public ConsultaUsuarioController(DBClinicaAcmeContext db)
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
            lista = listaUsuario;
            return listaUsuario;
        }


        public IActionResult Index()
        {
            listaUsuario = listarUsuarios();
            return View(listaUsuario);
        }

        public FileResult exportarExcel()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Nombre", "Tipo Usuario", "Password" };
            string[] nombrePropiedades = { "Nombre", "TipoUsuarioNombre", "Password"};
            byte[] buffer = util.generarExcel(cabeceras, nombrePropiedades, lista);
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public FileResult exportarPDF()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Nombre", "Tipo Usuario", "Password" };
            string[] nombrePropiedades = { "Nombre", "TipoUsuarioNombre", "Password" };
            string titulo = "Reporte de usuarios";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }

    }
}
