using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Core.Dtos
{
    public interface IDtoBase
    {
        public int Id { get; set; }
        DateTime? CreatedDate { get; }
        string CreatedBy { get; }
        DateTime? UpdatedDate { get; }
        string UpdatedBy { get; }
        public bool IsDelete { get; set; }
    }
}
