using System;
using System.ComponentModel.DataAnnotations;

namespace PrácticaEnElAula.Models
{
    public class Vacations
    {
        public int Id { get; set; }

        public int IdEmployee { get; set; }
        [Display(Name = "Fecha de Salida")]
        public DateTime FechaDeSalida { get; set; }
        [Display(Name = "Fecha de ingreso")]
        public DateTime FechaDeIngreso { get; set; }
        [Display(Name = "Empleado")]
        public Employee Employee { get; set; }
    }
}
