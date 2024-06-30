using System;
using DesafioFindesAPI.Models;

namespace DesafioFindesAPI.Infra.Repositories.Interfaces
{
	public interface IAttendanceRepository
	{
        Task<List<Consultation>> GetAllAttendancesAsync();
        Task SaveAttendanceAsync(Consultation consultation);
        Task SaveAttendancesToFileAsync(List<Consultation> consultations);
        Task DeleteAttendanceAsync(int attendanceId);
        Task UpdateAttendanceAsync(Consultation consultation);
        Task<List<Consultation>> GetAllAttendancesByUserIdAsync(int userId, bool isAdmin, bool isDoctor);
    }
}

