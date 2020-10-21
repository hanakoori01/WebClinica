using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClinica.Models
{
    public class Enfermedad
    {
        [Required(ErrorMessage = "Debe digitar el Id del usuario")]
        [Display(Name = "Enfermedad Id:")]
        public int? EnfermedadId { get; set; }
        [Required(ErrorMessage = "Debe digitar el nombre del usuario")]
        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }
        [Display(Name = "Descripcion:")]
        [Required(ErrorMessage = "Debe digitar la descripción de la Especialidad")]
        public string Descripcion { get; set; }
    }
}
