using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.API.DTOs
{
    [Table("HRCorporateTitle")]
    public class HRCorporateTitleDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Column("Level_Grade")]
        public string LevelGrade { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(9,2)")]
        public decimal MinBasicSalary { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal MaxBasicSalary { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal MinAllowance { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal MaxVehicleAllowance { get; set; }

        public bool IsActive { get; set; }

        [Column("Created_On")]
        public DateTime CreatedOn { get; set; }

        [Column("Updated_On")]
        public DateTime UpdatedOn { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? MaxAllowance { get; set; }

        [Required]
        [StringLength(5)]
        public string ShortName { get; set; } = string.Empty;

        [Column("Created_By")]
        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; } = string.Empty;

        [Column("Updated_By")]
        [Required]
        [StringLength(100)]
        public string UpdatedBy { get; set; } = string.Empty;

        [Required]
        [StringLength(16)]
        public string ClientIP { get; set; } = string.Empty;
    }
}