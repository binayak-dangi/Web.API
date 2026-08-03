namespace Web.API.Models.DTOS.Setup
{
    public class HRPermissionEmployeeRoleDto
    {
        public long Id { get; set; }
        public long IdHRCompany { get; set; }
        public long? IdHREmployee { get; set; }
        public long? IdHRRole { get; set; }
        public long? IdHRPermission { get; set; }

        public bool CreateOnly { get; set; }
        public bool ReadOnly { get; set; }
        public bool EditOnly { get; set; }
        public bool DeleteOnly { get; set; }

        public long? IdParentPermission { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }

        public string? Area { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }

        public string? PermissionCode { get; set; }

        public long? ParentOrder { get; set; }
        public long? ChildOrder { get; set; }

        public string? FontIcon { get; set; }
        public string? AreaIcon { get; set; }

        public bool IsActive { get; set; }

        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }

        public DateTime? Created_On { get; set; }
        public DateTime? Updated_On { get; set; }
        public Guid IdGuid { get; set; }
        public bool IsDeleted { get; set; }
    }
}
