namespace DownNotifier.Core.Dtos
{
    public class DtoBase : IDtoBase
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDelete { get; set; }
    }
}
