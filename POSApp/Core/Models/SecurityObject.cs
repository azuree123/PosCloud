
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POSApp.Persistence;

namespace POSApp.Core.Models
{
    [Table("SecurityObjects", Schema = PosDbContext.DEFAULT_SCHEMA)]
    public class SecurityObject : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SecurityObjectId { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }// varchar 150
        [MaxLength(150)]
        public string Type { get; set; }// Form, report etc // varchar 15
        [MaxLength(150)]
        public string Module { get; set; }//HR, Finane etc 50

        public virtual ICollection<SecurityRight> SecurityRights { get; set; }

    }
}
