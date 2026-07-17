using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.API.DTOs
{
    [Table("HRCorporateTitle")]
    public class HRCorporateTitleDto
    {
        [Key]
        public long Id { get; set; }

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

      
        [Column(TypeName = "decimal(9,2)")]
        public decimal? MaxAllowance { get; set; }

        [Required]
        [StringLength(5)]
        public string ShortName { get; set; } = string.Empty;

        [Required]
        [StringLength(16)]
        public string ClientIP { get; set; } = string.Empty;
    }
}