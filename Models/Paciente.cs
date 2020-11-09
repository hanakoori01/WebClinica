using Clinica.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClinica.Models
{
    public partial class Paciente
    {
        [Required(ErrorMessage = "Debe digitar la identificación del paciente")]
        [Display(Name = "Identificador:")]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "Debe digitar el nombre del paciente")]
        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe digitar el apellido del paciente")]
        [Display(Name = "Apellido:")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Debe digitar la dirección del paciente")]
        [Display(Name = "Dirección:")]
        [StringLength(200, ErrorMessage = "Ha excedido los 200 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe digitar el télefono")]
        [Display(Name = "Télefono:")]
        public string TelefonoContacto { get; set; }

        [Display(Name = "Email:")]
        [EmailAddress(ErrorMessage = "Debe ingresar un email válido")]
        public string Email { get; set; }

        public virtual ICollection<Citas> Citas { get; set; }
    }
}
