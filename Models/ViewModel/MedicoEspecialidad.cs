using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClinica.Models.ViewModel
{
    public class MedicoEspecialidad
    {
        [Required(ErrorMessage = "Debe digitar el ID del medico")]
        [Display(Name = "ID:")]
        public int MedicoId { get; set; }
        [Required(ErrorMessage = "Debe digitar el nombre del medico")]
        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe digitar el apellido del medico")]
        [Display(Name = "Apellidos:")]
        public string Apellidos { get; set; }
        [Display(Name = "Direccion:")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Debe digitar el numero de telfono del medico")]
        [Display(Name = "Telefono fijo:")]
        public string TelefonoFijo { get; set; }
        [Required(ErrorMessage = "Debe digitar el celular del medico")]
        [Display(Name = "Celular:")]
        public string TelefonoCelular { get; set; }
        [Required(ErrorMessage = "Debe ingresar la foto del medico")]
        [Display(Name = "Foto:")]
        public string Foto { get; set; }
        [Display(Name = "ID de especialidad:")]
        public int EspecialidadId { get; set; }
        public string Especialidad { get; set; }
        public string msgError { get; set; }

    }
}
