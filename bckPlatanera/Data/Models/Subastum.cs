using System;
using System.Collections.Generic;

namespace bckPlatanera.Data.Models
{
    public partial class Subastum
    {
        public Subastum()
        {
            CityHasSubasta = new HashSet<CityHasSubastum>();
            Inscriptions = new HashSet<Inscription>();
        }

        public int IdSubasta { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
        public double InitialPrice { get; set; }
        public string Photos { get; set; } = null!;
        public string BananaType { get; set; } = null!;
        public string DescriptionProduct { get; set; } = null!;
        public double MeasurementUnits { get; set; }
        public string PersonDocumentNumber { get; set; } = null!;

        public virtual Person PersonDocumentNumberNavigation { get; set; } = null!;
        public virtual ICollection<CityHasSubastum> CityHasSubasta { get; set; }
        public virtual ICollection<Inscription> Inscriptions { get; set; }
    }
}
