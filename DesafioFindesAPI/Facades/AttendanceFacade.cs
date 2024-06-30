using System;
using DesafioFindesAPI.Facades.Interfaces;
using DesafioFindesAPI.Infra.Repositories;
using DesafioFindesAPI.Infra.Repositories.Interfaces;
using DesafioFindesAPI.Models;
using DesafioFindesAPI.Models.Dtos;

namespace DesafioFindesAPI.Facades
{
	public class AttendanceFacade : IAttendanceFacade
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IUserRepository _userRepository;

        public AttendanceFacade()
		{
            _attendanceRepository = new AttendanceRepository();
            _userRepository = new UserRepository();
		}

        public async Task<string> CreateConsultation(Consultation consultation)
        {
           var user = _userRepository.GetUserByIdAsync(consultation.AttendantId);

           if(user == null || (user.Result.Role != "Atendente" && user.Result.Role != "Admin"))
           {
               return "Usuário não tem permissão!";
           }

           var consultations = await _attendanceRepository.GetAllAttendancesAsync();

           bool isConflict = consultations.Any(c =>
             c.Date == consultation.Date &&
             c.DoctorId == consultation.DoctorId);

            if (isConflict)
            {
                return "Já existe uma consulta marcada para este horário com o mesmo médico.";
            }
           await _attendanceRepository.SaveAttendanceAsync(consultation);
            return "";
        }

        public async Task<string> DeleteConsultation(int userId, int attendanceId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            var consultations = await _attendanceRepository.GetAllAttendancesAsync();
            var attendance = consultations.Find(c => c.Id == attendanceId);

            if (attendance != null && user != null)
            {
                if(user.Role == "Admin")
                {
                    await _attendanceRepository.DeleteAttendanceAsync(attendanceId);
                    return "";
                }
                if (user.Role == "Atendente" && attendance.AttendantId == userId)
                {
                    await _attendanceRepository.DeleteAttendanceAsync(attendanceId);
                    return "";
                }
            }

            return "Erro ao deletar consulta!";
        }

        public async Task<List<Consultation>> GetConsultations(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if(user != null && user.Role == "Admin")
            {
                return await _attendanceRepository.GetAllAttendancesByUserIdAsync(userId, true, false);
            }

            if (user != null && user.Role == "Medico")
            {
                return await _attendanceRepository.GetAllAttendancesByUserIdAsync(userId, false, true);
            }

            return await _attendanceRepository.GetAllAttendancesByUserIdAsync(userId, false, false);
        }

        public async Task<List<UserDto>> GetDoctors()
        {
            var users = await _userRepository.GetUserByRoleAsync("Medico");
            var userDtos = users.Select(u => new UserDto { Id = u.Id, Username = u.Username }).ToList();
            return userDtos;
        }

        public async Task<string> UpdateStatusConsultation(int userId, ConsultationDtos consultation)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var consultations = await _attendanceRepository.GetAllAttendancesAsync();
            var attendance = consultations.Find(c => c.Id == consultation.Id);

            if(attendance != null && (userId == attendance.DoctorId || user.Role == "Admin"))
            {
                attendance.updateStatus(consultation.IsAttended);
                await _attendanceRepository.UpdateAttendanceAsync(attendance);
                return "";
            }

            return "Erro ao atualizar consulta!";
        }
    }
}

