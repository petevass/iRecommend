using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace iRecommendWeb.Models
{
    [Table("recommendation_letters")]
    [Index(nameof(letterId), IsUnique =true)]
    public class RecLetterRequest
    {

        [Key]
        public Guid letterId { get; set; } = Guid.CreateVersion7();

        [Column("student_id")]
        public string studentId { get; set; }

        [Column("teacher_id")]
       public  string teacherId { get; set; }


        [Column("pool_id")]
        public Guid poolId { get; set; }


        [Column("submision_method")]
        public SubmitMethod method { get; set; }

        [Column("extra_details")]
       public string details { get; set; }

        [Column("deadline")]
        public DateTime deadline { get; set; }

        [Column("college_name")]
        public string college { get; set; }
        
        [Column("current_progress")]
        public ProgressLevel currentProgress { get; set; }

        public RecLetterRequest() { 
        

        
        }
    }

    public enum SubmitMethod
    {
        CommonApp,
        Coalition,
        QuestBridge,
        SchoolPlatform,   
        CollegePortal,
        Email,
        Paper,
        Other
    }

    public enum ProgressLevel{ 
    
        NotStarted,
        Started,
        Written,
        Submitted

    
    }
}
