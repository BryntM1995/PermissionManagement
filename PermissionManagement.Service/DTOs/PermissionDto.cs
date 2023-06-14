using PermissionManagement.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PermissionManagement.Service.DTOs
{
    public class PermissionDto : BaseDto
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        [ForeignKey("PermissionId")]
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
