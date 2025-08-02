using ClinicManagement.Models;
using ClinicManagement.Repositories;
using ClinicManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
        }

        public async Task<IActionResult> Add()
        {
            AppointmentWithPatientsViewModel appointmentWithPatientsVM = new AppointmentWithPatientsViewModel()
            {
                Patients = await _patientRepository.GetAllAsync()
            };

            return View("Add", appointmentWithPatientsVM);
        }

        public async Task<IActionResult> SaveAdd(AppointmentViewModel appointmentVM)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Appointment appointment = new Appointment()
                    {
                        PatientId = appointmentVM.PatientId,
                        AppointmentDate = appointmentVM.AppointmentDate,
                        Reason = appointmentVM.Reason
                    };

                    await _appointmentRepository.AddAsync(appointment);
                    await _appointmentRepository.SaveAsync();

                    return Ok();
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Appointment appointmentFromDB = await _appointmentRepository.GetByIdAsync(id);
            if(appointmentFromDB != null)
            {
                EditAppointmentViewModel appointmentVM = new EditAppointmentViewModel()
                {
                    Id = id,
                    PatientId = appointmentFromDB.PatientId,
                    AppointmentDate = appointmentFromDB.AppointmentDate,
                    Reason = appointmentFromDB.Reason,
                    Patients = await _patientRepository.GetAllAsync()
                };

                return View("Edit", appointmentVM);
            }
            return NotFound();
        }

        public async Task<IActionResult> SaveEdit(int id, EditAppointmentViewModel appointmentVM)
        {
            if(ModelState.IsValid)
            {
                Appointment appointmentFromDB = await _appointmentRepository.GetByIdAsync(id);
                if(appointmentFromDB != null)
                {
                    try
                    {
                        appointmentFromDB.PatientId = appointmentVM.PatientId;
                        appointmentFromDB.AppointmentDate = appointmentVM.AppointmentDate;
                        appointmentFromDB.Reason = appointmentVM.Reason;

                        await _appointmentRepository.SaveAsync();

                        return RedirectToAction("Index");
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);

                        appointmentVM.Patients = await _patientRepository.GetAllAsync();
                        return View("Edit", appointmentVM);
                    }
                }
                return NotFound();
            }
            appointmentVM.Patients = await _patientRepository.GetAllAsync();
            return View("Edit", appointmentVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Appointment appointmentFromDB = await _appointmentRepository.GetAppointmentWithPatientAsync(id);
            if(appointmentFromDB != null)
            {
                return View("Delete", appointmentFromDB);
            }
            return NotFound();
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            Appointment appointmentFromDB = await _appointmentRepository.GetByIdAsync(id);
            if(appointmentFromDB != null)
            {
                _appointmentRepository.Delete(appointmentFromDB);
                await _appointmentRepository.SaveAsync();

                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
