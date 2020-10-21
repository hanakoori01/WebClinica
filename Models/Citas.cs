using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebClinica.Models;

namespace Clinica.Models
{
    public partial class Citas
    {
        [Display(Name = "Cita Id:")]
        public int? CitaId { get; set; }
        [Display(Name = "Paciente Id:")]
        public int? PacienteId { get; set; }
        [Display(Name = "Médico Id:")]
        public int? MedicoId { get; set; }
        [Display(Name = "Fecha Cita:")]

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCita { get; set; }
        public string Diagnostico { get; set; }
        [Display(Name = "Especialidad Id:")]
        public int? EspecialidadId { get; set; }

        public virtual Medico Medico { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
}
