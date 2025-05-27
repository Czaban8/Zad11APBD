namespace WebApplication1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table( "Perscription" )]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    [ForeignKey(nameof(Doctor))]
    public int IdDoctor { get; set; }
    [ForeignKey(nameof(Patient))]
    public int IdPatient { get; set; }
        
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
}