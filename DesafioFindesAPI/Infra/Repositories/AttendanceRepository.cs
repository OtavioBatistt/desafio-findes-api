using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DesafioFindesAPI.Infra.Repositories.Interfaces;
using DesafioFindesAPI.Models;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly string _filePath;

    public AttendanceRepository()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "LocalStorage/attendances.json");
        _filePath = filePath;
    }

    public async Task<List<Consultation>> GetAllAttendancesAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Consultation>();
        }

        var jsonData = await File.ReadAllTextAsync(_filePath);
        try
        {
            return JsonSerializer.Deserialize<List<Consultation>>(jsonData);
        }
        catch(Exception e)
        {
            var teste = e;
        }
        return JsonSerializer.Deserialize<List<Consultation>>(jsonData);
    }

    public async Task<List<Consultation>> GetAllAttendancesByUserIdAsync(int userId, bool isAdmin, bool isDoctor)
    {
        var consultations = await GetAllAttendancesAsync();

        if(isAdmin)
        {
            return consultations;
        }

        if (isDoctor)
        {
            return consultations.Where(c => c.DoctorId == userId).ToList();
        }

        return consultations.Where(c => c.AttendantId == userId).ToList();
    }

    public async Task SaveAttendanceAsync(Consultation consultation)
    {
        var consultations = await GetAllAttendancesAsync();
        consultation.Id = consultations.Count > 0 ? consultations[^1].Id + 1 : 1;
        consultations.Add(consultation);
        await SaveAttendancesToFileAsync(consultations);
    }

    public async Task SaveAttendancesToFileAsync(List<Consultation> consultations)
    {
        var jsonData = JsonSerializer.Serialize(consultations);
        await File.WriteAllTextAsync(_filePath, jsonData);
    }

    public async Task DeleteAttendanceAsync(int attendanceId)
    {
        var consultations = await GetAllAttendancesAsync();
        var itemToRemove = consultations.Find(c => c.Id == attendanceId);

        if (itemToRemove != null)
        {
            consultations.Remove(itemToRemove);
            await SaveAttendancesToFileAsync(consultations);
        }
    }

    public async Task UpdateAttendanceAsync(Consultation consultation)
    {
        var consultations = await GetAllAttendancesAsync();

        var index = consultations.FindIndex(c => c.Id == consultation.Id);

        if (index != -1)
        {
            consultations[index] = consultation;
            await SaveAttendancesToFileAsync(consultations);
        }
    }
}
