using Clinica.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClinica.Filter;
using WebClinica.Models;

namespace WebClinica.Controllers.Consultas
{
    [ServiceFilter(typeof(Seguridad))]
    public class ConsultaTipoUsuarioController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<TipoUsuario> listaTipoUsuario = new List<TipoUsuario>();
        static List<TipoUsuario> lista = new List<TipoUsuario>();

        public ConsultaTipoUsuarioController(DBClinicaAcmeContext db)
        {
            _db = db;
        }

        public List<TipoUsuario> listarTipoUsuarios()
        {
            listaTipoUsuario = (from _tipoUsuario in _db.TipoUsuario

                            select new TipoUsuario
                            {
                               TipoUsuarioId = _tipoUsuario.TipoUsuarioId,
                               Nombre = _tipoUsuario.Nombre,
                                Descripcion = _tipoUsuario.Descripcion
                            }).ToList();
            lista = listaTipoUsuario;
            return listaTipoUsuario;
        }


        public IActionResult Index()
        {
            listaTipoUsuario = listarTipoUsuarios();
            return View(listaTipoUsuario);
        }

        public FileResult exportarExcel()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Nombre", "Descripción"};
            string[] nombrePropiedades = { "Nombre", "Descripcion" };
            byte[] buffer = util.generarExcel(cabeceras, nombrePropiedades, lista);
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public FileResult exportarPDF()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = { "Nombre", "Descripción" };
            string[] nombrePropiedades = { "Nombre", "Descripcion" };
            string titulo = "Reporte de usuarios";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }
    }
}
