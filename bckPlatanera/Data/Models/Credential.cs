using System;
using System.Collections.Generic;

namespace bckPlatanera.Data.Models
{
    public partial class Credential
    {
        public int IdCredentials { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string NumberDocumentPerson { get; set; } = null!;

        public virtual Person NumberDocumentPersonNavigation { get; set; } = null!;
    }
}
