namespace WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
 
public class DatabaseContext : DbContext
{
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    protected DatabaseContext()
    {
    }
    
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor() { IdDoctor = 1, Email = "Doc1@gmail.com", FirstName = "Jan", LastName = "Dzban" },
            new Doctor() { IdDoctor = 2, Email = "Doc2@gmail.com", FirstName = "Piotr", LastName = "Lotr" },
            new Doctor() { IdDoctor = 3, Email = "Doc3@gmail.com", FirstName = "Tungtung", LastName = "Sahur" }
        });
        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new Patient()
                { IdPatient = 1, FirstName = "Tralalero", LastName = "Tralala", BirthDate = new DateTime(2005, 02, 12) },
            new Patient()
                { IdPatient = 2, FirstName = "Balerina", LastName = "Capuchina", BirthDate = new DateTime(2004, 11, 17) }
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament()
                { IdMedicament = 1, Name = "Anybul", Description = "Przeciwbul", Type = "Przeciwbulowe" },
            new Medicament()
                { IdMedicament = 2, Name = "Przeciwbul", Description = "Przeciwbul", Type = "Przeciwbulowe" },
        });

        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
        {
            new Prescription() { IdPrescription = 1, Date = new DateTime(2023, 11, 21), DueDate = new DateTime(2024, 12, 12), IdPatient = 1, IdDoctor = 1 },
            new Prescription() { IdPrescription = 2, Date = new DateTime(2023, 07, 13), DueDate = new DateTime(2024, 09, 05), IdPatient = 2, IdDoctor = 1 },
            new Prescription() { IdPrescription = 3, Date = new DateTime(2023, 04, 22), DueDate = new DateTime(2024, 06, 13), IdPatient = 2, IdDoctor = 3}
        });

        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>()
        {
            new PrescriptionMedicament() {IdMedicament = 1, IdPrescription = 1,Dose=2,Details = "LEK a"},
            new PrescriptionMedicament() {IdMedicament = 2, IdPrescription = 1,Dose=1,Details = "LEK b" },
            new PrescriptionMedicament() {IdMedicament = 1, IdPrescription = 2,Dose=3,Details = "LEK c" },
            new PrescriptionMedicament() {IdMedicament = 2, IdPrescription = 3,Dose=7,Details = "LEK d" },
        });
    }
    
}
    