using System.ComponentModel.DataAnnotations;
using ClinicManagement.Models;

namespace ClinicManagement.ViewModel
{
    public class EditAppointmentViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }

        public IEnumerable<Patient>? Patients { get; set; }
    }
}
