using Azure;
using Microsoft.EntityFrameworkCore;
using ServiceSchool.Domain.Repositories;
using ServiceSchool.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace ServiceSchool.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
               
        public StudentRepository(SchoolContext context, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        /// <summary>
        /// Crear un nuevo estudiante
        /// </summary>
        /// <param name="student">Informacion del estudiante</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<StudentResponse> AddStudentAsync(Students student)
        {
            try
            {
                StudentResponse response = new StudentResponse();
                List<Students> students = new List<Students>();
                var existing = await _context.Students.FirstOrDefaultAsync(s => s.DocumentNumberStudent == student.DocumentNumberStudent);
                if (existing != null)
                {
                    students.Add(existing);                    
                    response.Student = students;
                    response.Message = string.Format(Resources.Resource.ExistMessage, student.DocumentNumberStudent);
                    return response;
                }
                else
                {
                    _context.Students.Add(student);
                    students.Add(student);
                    response.Student = students;
                    response.Message = string.Format(Resources.Resource.CreateMessage, student.DocumentNumberStudent);
                    await _context.SaveChangesAsync();
                    return response;
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(string.Format(Resources.Resource.MessageException, "Creacion"), ex);
            }
        }

        /// <summary>
        /// Obtener todos los estudiantes con paginacion
        /// </summary>
        /// <param name="pageNumber">Numero de pagina</param>
        /// <param name="pageSize">Tamaño de pagina</param>
        /// <returns></returns>
        public async Task<StudentResponse> GetAllStudentsAsync(int pageNumber, int pageSize)
        {
            try
            {
                StudentResponse response = new StudentResponse();
                List<Students> students = await _context.Students
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                response.Student = students;
                response.Message = string.Format(Resources.Resource.MessageOk);
                return response;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(string.Format(Resources.Resource.MessageException, "Consulta"), ex);
            }
        }

        /// <summary>
        /// Obtener un estudiante por su numero de documento
        /// </summary>
        /// <param name="documentNumber">Numero de Documento</param>
        /// <returns></returns>
        public async Task<StudentResponse> GetStudentByIdAsync(string documentNumber)
        {
            try
            {
                StudentResponse response = new StudentResponse();
                List<Students> students = new List<Students>();
                Students student = await _context.Students.FirstOrDefaultAsync(s => s.DocumentNumberStudent == documentNumber);
                if (student == null)
                {
                    response.Message = string.Format(Resources.Resource.MessageNoFound, documentNumber);
                    return response;
                }
                else
                {
                    students.Add(student);
                response.Student=students;
                response.Message = string.Format(Resources.Resource.MessageFound, documentNumber);
                return response;

                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(string.Format(Resources.Resource.MessageException, "Consulta"), ex);
            }
        }

        /// <summary>
        /// Actualizar la informacion de un estudiante
        /// </summary>
        /// <param name="student">Informacion del estudiante</param>
        /// <returns></returns>
        public async Task<StudentResponse> UpdateStudentAsync(Students student)
        {
            try
            {
                StudentResponse response = new StudentResponse();
                List<Students> students = new List<Students>();
                var existing = await _context.Students.FirstOrDefaultAsync(s => s.DocumentNumberStudent == student.DocumentNumberStudent);
                if (existing == null)
                {                    
                    response.Message = string.Format(Resources.Resource.MessageNoFound, student.DocumentNumberStudent);
                    students.Add(existing);
                    response.Student = students;
                    response.HasError = false;
                    return response;
                }
                else
                {
                    existing.NameStudent = student.NameStudent;
                    existing.SurnameStuden = student.SurnameStuden;
                    existing.NameGuardian = student.NameGuardian;
                    existing.ContactGuardian = student.ContactGuardian;
                    existing.IdCourse = student.IdCourse;
                    students.Add( existing);
                    response.Student = students;
                    response.Message = Resources.Resource.MessageUpdate;
                    await _context.SaveChangesAsync();
                    response.HasError = true;
                    return response;
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(string.Format(Resources.Resource.MessageException, "Actualizacion"), ex);
            }
        }

        /// <summary>
        /// Eliminar un estudiante por su Numero de Documento
        /// </summary>
        /// <param name="DocumentNumber">Numero de Documento</param>
        /// <returns></returns>
        public async Task<StudentResponse> DeleteStudentAsync(string DocumentNumber)
        {
            try
            {
                StudentResponse response = new StudentResponse();
                List<Students> students = new List<Students>();
                Students existing = await _context.Students.FirstOrDefaultAsync(s => s.DocumentNumberStudent == DocumentNumber);
                if (existing == null)
                {
                    response.Message = string.Format(Resources.Resource.MessageNoFound, DocumentNumber);
                    students.Add(existing);
                    response.Student = students;
                    response.HasError = false;
                    return response;
                }
                else 
                {
                    _context.Students.Remove(existing);
                    students.Add(existing);
                    response.Student = students;
                    response.Message = Resources.Resource.MessajeDelete;
                    await _context.SaveChangesAsync();
                    response.HasError = true;
                    return response;
                }
               
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(string.Format(Resources.Resource.MessageException, "Eliminacion"), ex);
            }
        }

        /// <summary>
        /// Guardar informacion de log de procesos
        /// </summary>
        /// <param name="process">Informacion de proceso</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
         public async Task<bool> AddLogProcessAsync(LogProcessDt logRequest)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("LogProcessApi");
                var response = await client.PostAsJsonAsync("AddLogProcess", logRequest);

                response.EnsureSuccessStatusCode();

                var logResponse = await response.Content.ReadFromJsonAsync<bool>();
                return logResponse;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(string.Format(Resources.Resource.MessageException, "Creacion del Log"), ex);
            }
        }

        public async Task<LogResponse> GetLogProcessByHashAsync(string hash)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("LogProcessApi");
                var response = await client.GetAsync($"api/GetLogProcessByHash/{hash}");

                response.EnsureSuccessStatusCode();

                var logResponse = await response.Content.ReadFromJsonAsync<LogResponse>();
                return logResponse;
            }
            catch (Exception ex)
            {

                throw new Exception(string.Format(Resources.Resource.MessageException, "Creacion del Log"),ex);
            }
        }
    }
}