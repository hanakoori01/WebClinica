using Clinica.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClinica.Models
{
    public partial class Medico
    {
        [Required(ErrorMessage = "Debe digitar la identificación del médico")]
        [Display(Name = "Identificación:")]
        public int MedicoId { get; set; }

        [Required(ErrorMessage = "Debe digitar el nombre del médico")]
        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe digitar el apellido del médico")]
        [Display(Name = "Apellidos:")]
        public string Apellidos { get; set; }

       
        [Display(Name = "Direccion:")]
        [StringLength(200, ErrorMessage = "Ha excedido los 200 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe digitar el número de télefono del médico")]
        [Display(Name = "Télefono fijo:")]
        public string TelefonoFijo { get; set; }

        [Required(ErrorMessage = "Debe digitar el número del celular del médico")]
        [Display(Name = "Celular:")]
        public string TelefonoCelular { get; set; }


        [Display(Name = "Especialidad:")]
        public int EspecialidadId { get; set; }

        public virtual Especialidad Especialidad { get; set; }

        public virtual ICollection<Citas> Citas { get; set; }

        public Medico() 
        { 
            Citas = new HashSet<Citas>();
        }
    }
}
