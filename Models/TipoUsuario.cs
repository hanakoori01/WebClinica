using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public partial class TipoUsuario
    {

        [Display(Name = "Tipo Usuario Id:")]
        public int? TipoUsuarioId { get; set; }

        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }
       
    }
}
