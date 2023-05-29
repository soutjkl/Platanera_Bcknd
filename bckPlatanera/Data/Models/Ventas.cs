namespace bckPlatanera.Data.Models
{
    public class Ventas
    {
        public int IdSubasta { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
        public double InitialPrice { get; set; }

        public string BananaType { get; set; } = null!;

        public double MeasurementUnits { get; set; }
    }
}
