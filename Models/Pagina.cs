using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public partial class Pagina
    {
        [Display(Name ="Id Página")]
        public int PaginaId { get; set; }

        [Display(Name = "Mensaje")]
        [Required(ErrorMessage ="Debe escribir un mensaje")]
        public string Menu { get; set; }

        [Display(Name = "Nombre de la acción")]
        [Required(ErrorMessage = "Debe escribir un nombre de método de acción")]
        public string Accion { get; set; }

        [Display(Name = "Nombre del contrador")]
        [Required(ErrorMessage = "Debe escribir un nombre de controlador")]
        [MinLength(3,ErrorMessage = "El nombre debe tener una longitud mínima de 3")]
        [MaxLength(50, ErrorMessage = "El nombre debe tener una longitud máxima de 50")]
        public string Controlador { get; set; }

        public int BotonId { get; set; }

        public int? BotonHabilitado { get; set; }
    }
}
