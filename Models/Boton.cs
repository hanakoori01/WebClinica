using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public partial class Boton
    {
        public int BotonId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? BotonHabilitado { get; set; }
    }
}
