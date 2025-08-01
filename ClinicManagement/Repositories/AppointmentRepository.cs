using ClinicManagement.Data;
using ClinicManagement.Models;

namespace ClinicManagement.Repositories
{
	public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
	{
        public AppointmentRepository(ApplicationDbContext context) : base(context) { }
    }
}
