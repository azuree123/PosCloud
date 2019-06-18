
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace POSApp.Core.Models
{
    public class UserStore
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       

        public int ApplicationUserId { get; set; }
        [Key, Column(Order = 1)]
        
       
        public int StoreId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Store Store { get; set; }
    }
}