﻿using Clinica.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClinica.Models
{
    public partial class Medico
    {
        [Required(ErrorMessage = "Debe digitar el Id del medico")]
        [Display(Name = "Id:")]
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
        [Display(Name = "Id de especialidad:")]
        public int EspecialidadId { get; set; }

        public virtual Especialidad Especialidad { get; set; }

        //public string msgError { get; set; }

        public virtual ICollection<Citas> Citas { get; set; }
    }
}
