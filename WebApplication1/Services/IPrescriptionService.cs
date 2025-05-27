namespace WebApplication1.Services;
using WebApplication1.DTO;
public interface IPrescriptionService
{
    Task AddPrescription(AddPrescription request, int IdDoctor);
    Task<PatientDetail> GetPatientDetails(int patientId);
}