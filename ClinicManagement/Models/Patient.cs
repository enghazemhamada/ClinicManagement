using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Models
{
	public class Patient
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

		[Required]
		[StringLength(10)]
		public string Gender { get; set; }

		[StringLength(20)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

		public List<Appointment> Appointments { get; set; }
	}
}
