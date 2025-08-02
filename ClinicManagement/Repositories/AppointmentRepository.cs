using ClinicManagement.Data;
using ClinicManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Repositories
{
	public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
	{
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Appointment> GetAppointmentWithPatientAsync(int id)
        {
            return await _context.Appointments.Include(a => a.Patient).FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
