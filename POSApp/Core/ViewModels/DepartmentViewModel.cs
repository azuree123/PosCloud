using System;

namespace POSApp.Core.ViewModels
{
    public class DepartmentViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}