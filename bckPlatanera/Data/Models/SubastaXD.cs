namespace bckPlatanera.Data.Models
{
    public class SubastaXD
    {
        public int IdSubasta { get; set; }

        public DateTime DateStarted { get; set; }

        public DateTime DateEnded { get; set; }

        public double InitialPrice { get; set; }

        public string Photos { get; set; } = null!;
        public string BananaType { get; set; } = null!;

        public string DescriptionProduct { get; set; } = null!;

        public double MeasurementUnits { get; set; }

        public string PersonDocumentNumber { get; set; } = null!;
        public int CityIdCity { get; set; }
    }
}
