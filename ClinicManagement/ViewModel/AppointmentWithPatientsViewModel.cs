using ClinicManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.ViewModel
{
    public class AppointmentWithPatientsViewModel
    {
        [Display(Name = "Patient")]
        public int PatientId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }

        public IEnumerable<Patient> Patients { get; set; }
    }
}
