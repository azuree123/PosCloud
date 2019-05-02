
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
        public string Name { get; set; }
        [MaxLength(150)]
        public string Type { get; set; }
        [MaxLength(150)]
        public string Module { get; set; }

        public virtual ICollection<SecurityRight> SecurityRights { get; set; }

    }
}
