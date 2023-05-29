using System;
using System.Collections.Generic;

namespace bckPlatanera.Data.Models
{
    public partial class Purchaseoffer
    {
        public int Idpurchaseoffer { get; set; }
        public double PricePurchase { get; set; }
        public DateTime DatePurchase { get; set; }
        public int InscriptionIdInscription { get; set; }

        public virtual Inscription InscriptionIdInscriptionNavigation { get; set; } = null!;
    }
}
