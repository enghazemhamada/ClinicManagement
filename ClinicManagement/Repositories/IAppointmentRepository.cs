using ClinicManagement.Models;

namespace ClinicManagement.Repositories
{
	public interface IAppointmentRepository : IGenericRepository<Appointment>
	{
		Task<Appointment> GetAppointmentWithPatientAsync(int id);
	}
}
