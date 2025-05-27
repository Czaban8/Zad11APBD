namespace WebApplication1.DTO;

public class PatientDetail
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<PrescriptionResponse> Prescriptions { get; set; }
}

public class PrescriptionResponse
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorResponse Doctor { get; set; }
    public List<PrescriptionMedicamentResponse> Medicaments { get; set; }
}

public class DoctorResponse
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class PrescriptionMedicamentResponse
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}