using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractProj_ASP.Models.Entities
{
    [Table("Returns")]
    public class Returns
    {
        [Key]
        public int OrderId { get; set; }
        public string? Status { get; set; }
    }
}
