using System;
using System.Collections.Generic;

namespace bckPlatanera.Data.Models
{
    public partial class City
    {
        public int IdCity { get; set; }
        public string NameCity { get; set; } = null!;
        public int DepartmentsIdDepartments { get; set; }

        public virtual Department DepartmentsIdDepartmentsNavigation { get; set; } = null!;
    }
}
