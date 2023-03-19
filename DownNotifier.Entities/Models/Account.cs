using DownNotifier.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DownNotifier.Entities.Models
{
    [Table("Accounts")]
    public class Account : EntityBase
    {
        [Column(Order = 1)]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Column(Order = 2)]
        [Required]
        [StringLength(50)]
        public string MailAddress { get; set; }
        [Column(Order = 3)]
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
