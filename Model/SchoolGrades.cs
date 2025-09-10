namespace ServiceSchool.Model
{
    public class SchoolGrades
    {
        public int IdSchoolGrades { get; set; } // Primary key
        public int IdStudent { get; set; }
        public int SchoolGrade { get; set; }
        public DateTime Date { get; set; }
        // Add other properties as needed
    }
}
