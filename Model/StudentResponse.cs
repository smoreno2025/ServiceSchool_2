using ServiceSchool.Model;
namespace ServiceSchool.Model;
public class StudentResponse
{
    public List<Students>? Student { get; set; }
    public string Message { get; set; }
    public string Hash { get; set; }
    public bool HasError { get; set; }
}