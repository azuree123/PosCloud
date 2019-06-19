
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace POSApp.Core.Models
{
    public class UserStore
    {
        
        public string ApplicationUserId { get; set; }
        
        public int StoreId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Store Store { get; set; }
    }
}