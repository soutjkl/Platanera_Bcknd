using System;
using System.Collections.Generic;

namespace bckPlatanera.Data.Models
{
    public partial class Inscription
    {
        public Inscription()
        {
            Purchaseoffers = new HashSet<Purchaseoffer>();
        }

        public int IdInscription { get; set; }
        public DateTime DateInscription { get; set; }
        public string PersonDocumentNumber { get; set; } = null!;
        public int SubastaIdSubasta { get; set; }

        public virtual Person PersonDocumentNumberNavigation { get; set; } = null!;
        public virtual Subastum SubastaIdSubastaNavigation { get; set; } = null!;
        public virtual ICollection<Purchaseoffer> Purchaseoffers { get; set; }
    }
}
