namespace bckPlatanera.Data.Models
{
    public class Publication
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
        public string NamePerson { get; set; } = null!;
        public string NameCity { get; set; } = null!;
        public string NameDepartment { get; set; } = null!;

        public Boolean Run{ get; set; } 
        public Boolean Status { get; set; } 


    }
}
