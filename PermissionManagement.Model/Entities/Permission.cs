using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PermissionManagement.Model.Entities
{
    public class Permission : BaseEntity, ISoftDeleteEntity, IAuditEntity
    {
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        [ForeignKey("PermissionId")]
        public virtual IEnumerable<PermissionType> PermissionId { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
        
    }
}
