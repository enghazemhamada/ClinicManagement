using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement.Models
{
	public class Appointment
	{
		public int Id { get; set; }

		[ForeignKey("Patient")]
		public int PatientId { get; set; }

		[DataType(DataType.Date)]
		public DateTime AppointmentDate { get; set; }

		[StringLength(255)]
		public string Reason { get; set; }

		public Patient Patient { get; set; }
	}
}
