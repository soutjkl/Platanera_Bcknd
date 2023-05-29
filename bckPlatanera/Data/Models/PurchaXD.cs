namespace bckPlatanera.Data.Models
{
    public class PurchaXD
    {
        public double PricePurchase { get; set; } 
        public DateTime DatePurchase { get; set; }
        public string PersonDocumentNumber { get; set; } = null!;
        public int SubastaIdSubasta { get; set; }
    }
}
