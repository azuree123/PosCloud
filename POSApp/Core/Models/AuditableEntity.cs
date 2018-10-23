using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSApp.Core.Models
{
    public abstract class AuditableEntity
    {
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedOn { get; set; }

        public string CreatedById { get; set; }
     //   public ApplicationUser CreatedBy { get; set; }

        public string UpdatedById { get; set; }
      //  public ApplicationUser UpdatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedOn { get; set; }


        [DefaultValue(false)]
        public bool Synced { get; set; }
        //public string SyncedById { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? SyncedOn { get; set; }

        public string Code { get; set; }
    }
}