using System.ComponentModel.DataAnnotations;

namespace Web.API.DTOs
{
    public class HRRoleDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Role Name is required.")]
        [StringLength(100, ErrorMessage = "Role Name cannot exceed 100 characters.")]
        public string RoleName { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string Created_By { get; set; } = string.Empty;

        public string Updated_By { get; set; } = string.Empty;

        public DateTime Created_On { get; set; } = DateTime.Now;

        public DateTime Updated_On { get; set; } = DateTime.Now;

        public Guid IdGUID { get; set; } = Guid.NewGuid();
    }
}