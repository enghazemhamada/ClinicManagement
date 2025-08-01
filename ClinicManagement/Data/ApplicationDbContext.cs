using ClinicManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options) { }

		public DbSet<Patient> Patients { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
	}
}
