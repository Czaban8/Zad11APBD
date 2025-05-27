namespace WebApplication1.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table( "Doctor" )]
public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    [MaxLength( 100 )]
    public string FirstName { get; set; }
    [MaxLength( 100 )]
    public string LastName { get; set; }
    [MaxLength( 100 )]   
    public string Email { get; set; }
        
    public DateTime Birthdate { get; set; }
    
    public ICollection<Prescription> Prescriptions { get; set; }
}
