using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PoSCloudApp.Core.Models
{
    public class City:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public virtual State State { get; set; }
    }
}