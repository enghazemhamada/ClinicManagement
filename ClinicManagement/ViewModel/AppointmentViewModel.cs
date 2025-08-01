using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.ViewModel
{
    public class AppointmentViewModel
    {
        public int PatientId { get; set; }

        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }
    }
}
