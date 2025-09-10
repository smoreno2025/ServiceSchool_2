using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceSchool.Model;

namespace ServiceSchool.Domain.Repositories;
public interface IStudentRepository
{
    Task<StudentResponse> AddStudentAsync(Students student);
    Task<StudentResponse> GetAllStudentsAsync(int pageNumber, int pageSize);
    Task<StudentResponse?> GetStudentByIdAsync(string documentNumber);
    Task<StudentResponse> UpdateStudentAsync(Students student);
    Task<StudentResponse> DeleteStudentAsync(string documentNumber);
    Task<bool> AddLogProcessAsync(LogProcessDt process);
    Task<LogResponse> GetLogProcessByHashAsync(string process);
}