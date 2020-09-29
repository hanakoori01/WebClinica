using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebClinica.Models;

namespace Clinica.Models
{
    public partial class Citas
    {
        [Display(Name = "Cita ID")]
        public int CitaId { get; set; }
        [Display(Name = "Paciente ID")]
        public string PacienteId { get; set; }
        [Display(Name = "Médico ID")]
        public string MedicoId { get; set; }
        [Display(Name = "Fecha cita")]

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCita { get; set; }
        public string Diagnostico { get; set; }
        [Display(Name = "Especialidad ID")]
        public int? EspecialidadId { get; set; }

        public virtual Medico Medico { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
}
