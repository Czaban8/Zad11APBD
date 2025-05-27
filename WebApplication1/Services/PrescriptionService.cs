namespace WebApplication1.Services;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Models;

public class PrescriptionService : IPrescriptionService
{
    private readonly DatabaseContext _context;
    public PrescriptionService(DatabaseContext context)
    {
        _context = context;
    }
    public async Task AddPrescription(AddPrescription request, int IdDoctor)
    {
            if (request.DueDate < request.Date)
                throw new ArgumentException("DueDate must be greater than or equal to Date.");
            
            if (request.Medicaments.Count > 10)
                throw new ArgumentException("A prescription cannot contain more than 10 medicaments.");
            
            var medicamentIds = request.Medicaments.Select(m => m.IdMedicament).Distinct().ToList();
            var existingMedicaments = await _context.Medicaments
                .Where(m => medicamentIds.Contains(m.IdMedicament))
                .Select(m => m.IdMedicament)
                .ToListAsync();

            var missingMedicaments = medicamentIds.Except(existingMedicaments).ToList();
            if (missingMedicaments.Any())
                throw new KeyNotFoundException("One or more medicaments do not exist.");
            
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.IdPatient == request.Patient.IdPatient);
            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = request.Patient.FirstName,
                    LastName = request.Patient.LastName,
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }
            
            
            
            var prescription = new Prescription
            {
                Date = request.Date,
                DueDate = request.DueDate,
                IdDoctor = IdDoctor,
                IdPatient = patient.IdPatient,
                PrescriptionMedicaments = new List<PrescriptionMedicament>()
            };

            foreach (var med in request.Medicaments)
            {
                prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
                {
                    IdMedicament = med.IdMedicament,
                    Dose = med.Dose,
                    Details = med.Description
                });
            }

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
        }

        public async Task<PatientDetail> GetPatientDetails(int patientId)
        {
            var patient = await _context.Patients
                .Include(p => p.Prescriptions)
                    .ThenInclude(pres => pres.Doctor)
                .Include(p => p.Prescriptions)
                    .ThenInclude(pres => pres.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
                .FirstOrDefaultAsync(p => p.IdPatient == patientId);

            if (patient == null)
                throw new KeyNotFoundException("Patient not found.");
            
            var prescriptions = patient.Prescriptions.OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionResponse
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorResponse
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName,
                        LastName = p.Doctor.LastName
                    },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new PrescriptionMedicamentResponse
                    {
                        IdMedicament = pm.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose.Value,
                        Description = pm.Details
                    }).ToList()
                }).ToList();

            return new PatientDetail
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Prescriptions = prescriptions
            };
        }
}