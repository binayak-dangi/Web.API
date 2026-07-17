using MyTestMvc.Models.Setup;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.API.Models.Entities
{
    public class RefreshToken
    {
        public long Id { get; set; }

        public long IDHREmployee { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime Expires { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Revoked { get; set; }

        [ForeignKey(nameof(IDHREmployee))]
        public virtual HREmployee Employee { get; set; } = null!;
    }
}
