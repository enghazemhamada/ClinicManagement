using ClinicManagement.Models;
using ClinicManagement.Repositories;
using ClinicManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.Controllers
{
	public class PatientController : Controller
	{
		private readonly IPatientRepository _patientRepository;

		public PatientController(IPatientRepository patientRepository)
        {
			_patientRepository = patientRepository;
		}

        public async Task<IActionResult> Index()
        {
            return View("Index", await _patientRepository.GetAllAsync());
        }

        public IActionResult Add()
		{
			return View("Add");
		}

		public async Task<IActionResult> SaveAdd(PatientViewModel patientViewModel)
		{
			if(ModelState.IsValid)
			{
				Patient patient = new Patient()
				{
					Name = patientViewModel.Name,
					DateOfBirth = patientViewModel.DateOfBirth,
					Gender = patientViewModel.Gender,
					PhoneNumber = patientViewModel.PhoneNumber
				};

				await _patientRepository.AddAsync(patient);
				await _patientRepository.SaveAsync();

				return Ok();
			}
			return BadRequest(ModelState);
		}

		public async Task<IActionResult> Edit(int id)
		{
			Patient patientFromDB = await _patientRepository.GetByIdAsync(id);
			if(patientFromDB != null)
			{
				EditPatientViewModel patientVM = new EditPatientViewModel()
				{
					Id = id,
					Name = patientFromDB.Name,
					DateOfBirth = patientFromDB.DateOfBirth,
					Gender = patientFromDB.Gender,
					PhoneNumber = patientFromDB.PhoneNumber
				};

				return View("Edit", patientVM);
            }
			return NotFound();
		}

		public async Task<IActionResult> SaveEdit(int id, EditPatientViewModel patientVM)
		{
			if(ModelState.IsValid)
			{
                Patient patientFromDB = await _patientRepository.GetByIdAsync(id);
				if(patientFromDB != null)
				{
					patientFromDB.Name = patientVM.Name;
					patientFromDB.DateOfBirth = patientVM.DateOfBirth;
					patientFromDB.Gender = patientVM.Gender;
					patientFromDB.PhoneNumber = patientVM.PhoneNumber;

					await _patientRepository.SaveAsync();

					return RedirectToAction("Index");
				}
				return NotFound();
            }
			return View("Edit", patientVM);
		}

		public async Task<IActionResult> Delete(int id)
		{
            Patient patientFromDB = await _patientRepository.GetByIdAsync(id);
			if(patientFromDB != null)
			{
				return View("Delete", patientFromDB);
			}
			return NotFound();
        }

		public async Task<IActionResult> ConfirmDelete(int id)
		{
            Patient patientFromDB = await _patientRepository.GetByIdAsync(id);
			if(patientFromDB != null)
			{
				_patientRepository.Delete(patientFromDB);
				await _patientRepository.SaveAsync();

				return RedirectToAction("Index");
			}
			return NotFound();
        }
    }
}
