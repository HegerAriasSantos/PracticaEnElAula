using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrácticaEnElAula.Models
{
    public class Employee
    {
        public int Id { get; set; }
        
        public string Nombre { get; set; }

        public string Cargo { get; set; }

        [Display(Name = "Fecha de ingreso")]
        public DateTime FechaDeIngreso { get; set; }
        
        public List<Vacations> Vacations { get; set; }

    }
}
