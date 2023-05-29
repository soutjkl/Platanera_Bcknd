using System;
using System.Collections.Generic;

namespace bckPlatanera.Data.Models
{
    public partial class Session
    {
        public int IdSession { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public string Status { get; set; } = null!;
        public string PersonDocumentNumber { get; set; } = null!;

        public virtual Person PersonDocumentNumberNavigation { get; set; } = null!;
    }
}
