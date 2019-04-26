﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class CustomerModelView
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        [Display(Name = "Branch", ResourceType = typeof(Resource))]
        public string BranchName { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        public string Code { get; set; }
        [Required]
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        [Required]
        public string ArabicName { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Entered phone format like (abc@example.com)")]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Contact", ResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Resource))]
        public string Address { get; set; }
        [Display(Name = "State", ResourceType = typeof(Resource))]
        public string State { get; set; }
        [Display(Name = "City", ResourceType = typeof(Resource))]
        public string City { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }= DateTime.Today;
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}