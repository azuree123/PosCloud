using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSApp.Core.Models
{
    public class Client:AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Address { get; set; }
        [MaxLength(150)]
        public string Contact { get; set; }
        [MaxLength(150)]
        public string City { get; set; }
        [MaxLength(150)]
        public string State { get; set; }
        [MaxLength(150)]
        public string Image { get; set; }
     
    }
}