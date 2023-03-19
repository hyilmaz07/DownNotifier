using DownNotifier.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DownNotifier.Entities.Models
{
    [Table("Notifiers")]
    public class Notifier : EntityBase
    {
        [Column(Order = 1)]
        [Required]
        [StringLength(100)]
        public string Url { get; set; }
        [Column(Order = 2)]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(Order = 3)]
        [Required]
        public int Interval { get; set; }
        [Column(Order = 4)]
        [Required]
        [StringLength(50)]
        public string MailAddress { get; set; }
    }
}
