using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }
        [Display(Name = "Tipo Usuario Id:")]
        public int TipoUsuarioId { get; set; }

        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }

        public int BotonHabilitado { get; set; }
        public virtual ICollection<TipoUsuarioPagina> TipoUsuarioPagina { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
