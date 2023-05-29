namespace bckPlatanera.Data.Models
{
    public class Compras
    {
        public int IdSubasta { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
        public double PricePurchase { get; set; }
       
        public string BananaType { get; set; } = null!;
  
        public double MeasurementUnits { get; set; }
    }
}
