using DesafioFindesAPI.Facades;
using DesafioFindesAPI.Facades.Interfaces;
using DesafioFindesAPI.Models;
using DesafioFindesAPI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/attendance")]
public class ConsultationsController : ControllerBase
{
    private IAttendanceFacade _attendanceFacade;

    public ConsultationsController()
    {
        _attendanceFacade = new AttendanceFacade();
    }

    [HttpPost]
    public async Task<IActionResult> InsertConsultation(Consultation consultation)
    {
        var response = await _attendanceFacade.CreateConsultation(consultation);
        if (response != null && response.Length > 0)
        {
            return Unauthorized(response);
        }
        return Ok(consultation);
    }

    [HttpGet]
    public async Task<IActionResult> GetConsultations(int userId)
    {
        var response = await _attendanceFacade.GetConsultations(userId);

        return Ok(response.ToList());
    }

    
    [HttpDelete]
    public async Task<IActionResult> DeleteConsultation(int userId, int attendanceId)
    {
        var response = await _attendanceFacade.DeleteConsultation(userId, attendanceId);
        if (response != null && response.Length > 0)
        {
            return Unauthorized(response);
        }
        return Ok(response.ToList());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStatusConsultation(int userId, ConsultationDtos attendance)
    {
        var response = await _attendanceFacade.UpdateStatusConsultation(userId, attendance);
        if (response != null && response.Length > 0)
        {
            return Unauthorized(response);
        }
        return Ok(response.ToList());
    }


    [HttpGet("doctors")]
    public async Task<IActionResult> GetDoctors()
    {
        var response = await _attendanceFacade.GetDoctors();

        return Ok(response.ToList());
    }
}
