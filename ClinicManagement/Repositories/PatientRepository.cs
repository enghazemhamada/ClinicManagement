using ClinicManagement.Data;
using ClinicManagement.Models;

namespace ClinicManagement.Repositories
{
	public class PatientRepository : GenericRepository<Patient>, IPatientRepository
	{
        public PatientRepository(ApplicationDbContext context) : base(context) { }
    }
}
