using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.Models
{
    public abstract class AuditableEntity
    {
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }
        [DefaultValue(false)]
        public bool Synced { get; set; }
    }
}