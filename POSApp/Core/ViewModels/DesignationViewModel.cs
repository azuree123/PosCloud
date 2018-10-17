using System;

namespace POSApp.Core.ViewModels
{
    public class DesignationViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}