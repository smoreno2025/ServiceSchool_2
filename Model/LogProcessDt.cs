namespace ServiceSchool.Model
{
    public class LogProcessDt
    {
        public string Hash { get; set; } // Primary key
        public string TypeTransaction { get; set; }
        public string Request { get; set; }
        public string MessageResult { get; set; }
        public DateTime DateTransaction { get; set; }
    }
}
