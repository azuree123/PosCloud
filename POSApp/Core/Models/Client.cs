using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POSApp.Persistence;

namespace POSApp.Core.Models
{
        [Table("Clients", Schema = PosDbContext.DEFAULT_SCHEMA)]
    public class Client:AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Name { get; set; }
        [MaxLength(300)]
        public string Address { get; set; }
        [MaxLength(150)]
        public string Contact { get; set; }
        [MaxLength(150)]
        
        public string City { get; set; }
        [MaxLength(150)]
        public string State { get; set; }

    }
}