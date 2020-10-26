using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinica.Models.ViewModel
{
    public class CitaMedica
    {
        [Display(Name = "Cita Id")]
        public int CitaId { get; set; }

        [Display(Name = "Paciente Id")]
        public int PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public string NombrePaciente { get; set; }

        public int MedicoId { get; set; }

        [Display(Name = "Médico")]
        public string NombreMedico { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha cita")]
        public DateTime FechaCita { get; set; }

        public string Diagnostico { get; set; }

        public int EspecialidadId { get; set; }

        [Display(Name = "Especialidad")]
        public string NombreEspecialidad { get; set; }

        public int SelectedOption { get; set; }

        public IEnumerable<SelectListItem> Lista { get; set; }

    }
}
