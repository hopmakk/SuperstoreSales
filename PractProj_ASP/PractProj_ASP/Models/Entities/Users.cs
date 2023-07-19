using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractProj_ASP.Models.Entities
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public string Region { get; set; }

        [Key]
        public string Manager { get; set; }

    }
}
