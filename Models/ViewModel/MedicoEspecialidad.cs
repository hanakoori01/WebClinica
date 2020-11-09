using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClinica.Models.ViewModel
{
    public class MedicoEspecialidad
    {
        [Required(ErrorMessage = "Debe digitar la Identificación del medico")]
        [Display(Name = "Identificación:")]
        public int MedicoId { get; set; }

        [Required(ErrorMessage = "Debe digitar el nombre del médico")]
        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe digitar el apellido del médico")]
        [Display(Name = "Apellidos:")]
        public string Apellidos { get; set; }

        [Display(Name = "Direccion:")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe digitar el número de télefono del médico")]
        [Display(Name = "Télefono fijo:")]
        public string TelefonoFijo { get; set; }

        [Required(ErrorMessage = "Debe digitar el número del celular del médico")]
        [Display(Name = "Celular:")]
        public string TelefonoCelular { get; set; }


        [Display(Name = "Especialidad:")]
        public int EspecialidadId { get; set; }

        public string Especialidad { get; set; }

        public string msgError { get; set; }

        public static implicit operator List<object>(MedicoEspecialidad v)
        {
            throw new NotImplementedException();
        }
    }
}
