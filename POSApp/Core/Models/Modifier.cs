using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class Modifier
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ModifierOption> ModifierOptions { get; set; }      
    }
}