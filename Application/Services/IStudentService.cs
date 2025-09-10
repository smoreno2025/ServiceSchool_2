using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceSchool.Model;

namespace ServiceSchool.Application.Services
{
    public interface IStudentService
    {
        Task<StudentResponse> CreateStudentAsync(Students student);
        Task<StudentResponse> GetAllStudentsAsync(int pageNumber, int pageSize);
        Task<StudentResponse> GetStudentByIdAsync(string documentNumber);
        Task<StudentResponse> UpdateStudentAsync(Students student);
        Task<StudentResponse> DeleteStudentAsync(string documentNumnber);
        Task<bool> AddLogProcessAsync(LogProcessDt process);
        Task<LogResponse> GetLogProcessByHashAsync(string process);
    }
}