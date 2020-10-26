using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public partial class Usuario
    {
        [Display(Name = "Usuario Id:")]
        public int UsuarioId { get; set; }

        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe digitar la contraseña del usuario")]
        [Display(Name = "Contraseña:")]
        public string Password { get; set; }

        [Display(Name = "Tipo de Usuario Id:")]
        public int TipoUsuarioId { get; set; }

        [Display(Name = "Tipo de Usuario:")]
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
