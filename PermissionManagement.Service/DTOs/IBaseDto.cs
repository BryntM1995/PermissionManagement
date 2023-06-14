using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionManagement.Service.DTOs
{
    public interface IBaseDto
    {
        public int Id { get; set; }
    }
    public class BaseDto : IBaseDto
    {
        public int Id { get; set; }
    }
}
