using System;
using System.Collections.Generic;

namespace bckPlatanera.Data.Models
{
    public partial class CityHasSubastum
    {
        public int CityIdCity { get; set; }
        public int SubastaIdSubasta { get; set; }

        public virtual Subastum SubastaIdSubastaNavigation { get; set; } = null!;
    }
}
