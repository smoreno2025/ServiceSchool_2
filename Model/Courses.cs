namespace ServiceSchool.Model
{
    public class Courses
    {
        public int IdCourse { get; set; } // Primary key
        public string NameCourse { get; set; }
        public int AmountStudents { get; set; }
        public int IdTeacher { get; set; } // Foreign key to Teachers
        // Add other properties as needed
    }
}
