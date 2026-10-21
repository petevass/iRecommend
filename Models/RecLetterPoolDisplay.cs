using System.ComponentModel.DataAnnotations.Schema;

namespace iRecommendWeb.Models
{
    public class RecLetterPoolDisplay
    {

        public string poolName { get; set; }
        

        public Guid poolId { get; set; }

       
        public List<ReqLetterRequestDisplay> requests { get; set; }

        [Column("pool_closing_date")]
        public DateTime closingDate { get; set; }



        public RecLetterPoolDisplay(string poolName, List<ReqLetterRequestDisplay> requests, DateTime closingDate) {

            this.poolName = poolName;
            this.requests = requests;
            this.closingDate = closingDate;
        
        }
    }

}
