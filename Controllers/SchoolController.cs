using ServiceSchool.Resources;
using Microsoft.AspNetCore.Mvc;
using ServiceSchool.Application.Services;
using ServiceSchool.Infrastructure.Security;

using System.Text.Json;
using ServiceSchool.Model;

namespace ServiceSchool.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly IStudentService _studentService;
        HashHelper HashHelper = new HashHelper();
        public SchoolController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                StudentResponse students = await _studentService.GetAllStudentsAsync(1,10);
                LogProcessDt log = new LogProcessDt
                {
                    MessageResult = students.Message,
                    Request = JsonSerializer.Serialize(students),
                    DateTransaction = DateTime.Now,
                    TypeTransaction = "Get"
                };
                string json = JsonSerializer.Serialize(log);
                log.Hash =HashHelper.CreateHash(json);
                students.Hash = log.Hash;
                bool createdLog = await _studentService.AddLogProcessAsync(log);

                return Ok(students);
            }
            catch (System.Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, string.Format(Resource.MessageException, "Consulta"));
            }
        }
        [HttpGet("GetStudentByDocument")]
        public async Task<IActionResult> GetStudentById(string DocumentNumber)
        {
            try
            {
                var students = await _studentService.GetStudentByIdAsync(DocumentNumber);
                LogProcessDt log = new LogProcessDt
                {
                    MessageResult = students.Message,
                    Request = JsonSerializer.Serialize(students),
                    DateTransaction = DateTime.Now,
                    TypeTransaction = "Get"
                };
                string json = JsonSerializer.Serialize(log);
                log.Hash = HashHelper.CreateHash(json);
                students.Hash = log.Hash;
                bool createdLog = await _studentService.AddLogProcessAsync(log);

                return Ok(students);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, string.Format(Resource.MessageException,"Consulta"));
            }
        }

        [HttpPost("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] Students student)
        {
            try
            {
                var createdStudent = await _studentService.CreateStudentAsync(student);
                LogProcessDt log = new LogProcessDt
                {
                    MessageResult = createdStudent.Message,
                    Request = JsonSerializer.Serialize(createdStudent),
                    DateTransaction = DateTime.Now,
                    TypeTransaction = "Post"
                };
                string json = JsonSerializer.Serialize(log);
                log.Hash = HashHelper.CreateHash(json);
                createdStudent.Hash = log.Hash;
                bool createdLog = await _studentService.AddLogProcessAsync(log);
                return CreatedAtAction(nameof(CreateStudent), new { createdStudent.Student }, createdStudent);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,string.Format(Resource.MessageException,"Creacion"));
            }
        }

        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Students student)
        {
            try
            {
                Task<StudentResponse> updateResult = _studentService.UpdateStudentAsync(student);
                if (updateResult.IsCompletedSuccessfully)
                {
                    LogProcessDt log = new LogProcessDt
                    {
                        MessageResult = updateResult.Result.Message,
                        Request = JsonSerializer.Serialize(updateResult.Result.Student),
                        DateTransaction = DateTime.Now,
                        TypeTransaction = "Put"
                    };
                    string json = JsonSerializer.Serialize(log);
                    log.Hash = HashHelper.CreateHash(json);
                    updateResult.Result.Hash = log.Hash;
                    bool createdLog = await _studentService.AddLogProcessAsync(log);
                    return Ok(updateResult);
                }
                else
                {
                    return Ok();
                }                
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, string.Format(Resource.MessageException, "Actualizacion"));
            }
        }

        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(string documentNumber)
        {
            try
            {
                StudentResponse deleteResult = await _studentService.DeleteStudentAsync(documentNumber);

                LogProcessDt log = new LogProcessDt
                {
                    MessageResult = deleteResult.Message,
                    Request =JsonSerializer.Serialize(deleteResult.Student),
                    DateTransaction = DateTime.Now,
                    TypeTransaction = "Delete"
                };
                string json = JsonSerializer.Serialize(log);
                log.Hash = HashHelper.CreateHash(json);
                deleteResult.Hash = log.Hash;
                bool createdLog = await _studentService.AddLogProcessAsync(log);
                return Ok(deleteResult);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, string.Format(Resource.MessageException, "Eliminacion"));
            }
        }

        
    }
}
