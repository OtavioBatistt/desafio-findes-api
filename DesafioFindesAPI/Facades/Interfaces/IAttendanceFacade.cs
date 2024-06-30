using System;
using DesafioFindesAPI.Models;
using DesafioFindesAPI.Models.Dtos;

namespace DesafioFindesAPI.Facades.Interfaces
{
	public interface IAttendanceFacade
	{
        Task<string> CreateConsultation(Consultation consultation);
        Task<List<UserDto>> GetDoctors();
        Task<List<Consultation>> GetConsultations(int userId);
        Task<string> DeleteConsultation(int userId, int attendanceId);
        Task<string> UpdateStatusConsultation(int userId, ConsultationDtos consultation);
    }
}

