using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace iRecommendWeb.Models
{
    [Table("letter_pools")]
    [Index(nameof(poolId), IsUnique = true)]
    public class RecLetterRequestPool
    {

        [Key]
        public Guid poolId { get; set; } = Guid.CreateVersion7();

        [Column("pool_name")]
        public string poolName { get; set; }

        [Column("teacher_id")]
        public string teacherId { get; set; }

        [Column("request_ids")]
        public List<Guid> requestIds { get; set; }

        [Column("pool_closing_date")]
        public DateTime closingDate { get; set; }

        public RecLetterRequestPool() { 
        
            

        }

    }
}
