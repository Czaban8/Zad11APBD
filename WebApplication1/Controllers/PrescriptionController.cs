using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;
using WebApplication1.Services;
using WebApplication1.DTO;


[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost("{IdDoctor}")]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescription request, int IdDoctor)
    {
        try
        {
            await _prescriptionService.AddPrescription(request, IdDoctor);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
