using System;
namespace DesafioFindesAPI.Models
{
	public class Consultation
	{
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int AttendantId { get; set; }
        public DateTime Date { get; set; }
        public bool IsAttended { get; set; }

        public void updateStatus(bool status)
        {
            this.IsAttended = status;
        }
    }
}

