using System;
using System.Collections.Generic;

namespace bckPlatanera.Data.Models
{
    public partial class Department
    {
        public Department()
        {
            Cities = new HashSet<City>();
        }

        public int IdDepartments { get; set; }
        public string NameDepartments { get; set; } = null!;

        public virtual ICollection<City> Cities { get; set; }
    }
}
