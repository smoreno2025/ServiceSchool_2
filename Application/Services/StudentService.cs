using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceSchool.Model;
using ServiceSchool.Domain.Repositories;
using ServiceSchool.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<StudentResponse> CreateStudentAsync(Students student)
        => await _repository.AddStudentAsync(student);

    public async Task<StudentResponse> GetAllStudentsAsync(int pageNumber, int pageSize)
        => await _repository.GetAllStudentsAsync(pageNumber,pageSize);

    public async Task<StudentResponse> GetStudentByIdAsync(string documentNumber)
        => await _repository.GetStudentByIdAsync(documentNumber);

    public async Task<StudentResponse> UpdateStudentAsync(Students student)
        => await _repository.UpdateStudentAsync(student);

    public async Task<StudentResponse> DeleteStudentAsync(string documentNumber)
        => await _repository.DeleteStudentAsync(documentNumber);

    public async Task<bool> AddLogProcessAsync(LogProcessDt process)
        => await _repository.AddLogProcessAsync(process);

    public async Task<LogResponse> GetLogProcessByHashAsync(string process)
        => await _repository.GetLogProcessByHashAsync(process);
}