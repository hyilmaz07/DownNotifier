namespace DownNotifier.Core.Entities
{
    public interface IEntityBase
    {
        public int Id { get; set; }
        DateTime? CreatedDate { get; }
        string CreatedBy { get; }
        DateTime? UpdatedDate { get; }
        string UpdatedBy { get; }
        public bool IsDelete { get; set; }
    }
}
