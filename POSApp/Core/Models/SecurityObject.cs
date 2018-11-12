
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSApp.Core.Models
{
    public class SecurityObject : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SecurityObjectId { get; set; }
        public string Name { get; set; }// varchar 150
        public string Type { get; set; }// Form, report etc // varchar 15
        public string Module { get; set; }//HR, Finane etc 50



    }
}
