using System.ComponentModel.DataAnnotations.Schema;

namespace iRecommendWeb.Models
{
    public class ReqLetterRequestDisplay
    {

        public Guid letterId { get; set; }

        public string studentName { get; set; }

        public string studentId { get; set; }

        public string teacherId { get; set; }

        public Guid poolId { get; set; }
       
        public SubmitMethod method { get; set; }

        public string details { get; set; }

        public DateTime deadline { get; set; }

   
        public string college { get; set; }
        
        public ProgressLevel currentProgress { get; set; }

        public ReqLetterRequestDisplay(
            Guid letterid,string studentName, string studentid, string teacherid, Guid poolid, 
            SubmitMethod method, string details, DateTime deadline, string college, ProgressLevel progress) {

            this.letterId = letterid;
            this.studentName = studentName;
            this.studentId = studentid;
            this.teacherId = teacherid;
            this.poolId = poolid;
            this.method = method;
            this.details = details;
            this.deadline = deadline;
            this.college = college;
            this.currentProgress = progress;

        
        }

    }
}
