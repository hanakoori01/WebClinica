using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinica.Models.ViewModel
{
    public class UsuarioTipoUsuario
    {
        [Display(Name = "Usuario Id:")]
        public int UsuarioId { get; set; }

        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }

        [Display(Name = "Contraseña:")]
        public string Password { get; set; }

        [Display(Name = "Tipo Usuario Id:")]
        public int TipoUsuarioId { get; set; }

        [Display(Name = "Tipo Usuario Nombre:")]
        public string TipoUsuarioNombre { get; set; }
    }
}
