using System.ComponentModel.DataAnnotations;

namespace WebClinica.Models
{
    public partial class Paciente
    {
        [Required(ErrorMessage = "Debe digitar el ID del paciente")]
        [Display(Name = "ID:")]
        public int PacienteId { get; set; }
        [Required(ErrorMessage = "Debe digitar el nombre del paciente")]
        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe digitar el apellido del paciente")]
        [Display(Name = "Apellido:")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Debe digitar la direccion del paciente")]
        [Display(Name = "Direccion:")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Debe digitar el telefono")]
        [Display(Name = "Telefono:")]
        public string TelefonoContacto { get; set; }
        public string Foto { get; set; }
    }
}
