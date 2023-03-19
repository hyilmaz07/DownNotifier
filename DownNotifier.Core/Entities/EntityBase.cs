using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DownNotifier.Core.Entities
{
    public abstract class EntityBase: IEntityBase
    {
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(Order = 60)]
        public DateTime? CreatedDate { get; set; }
        [Column(Order = 65)]
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Column(Order = 70)]
        public DateTime? UpdatedDate { get; set; }
        [Column(Order = 75)]
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        [Column(Order = 50)]
        public bool IsDelete { get; set; }
    }
}
