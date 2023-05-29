using System;
using System.Collections.Generic;

namespace bckPlatanera.Data.Models
{
    public partial class Person
    {
        public Person()
        {
            Credentials = new HashSet<Credential>();
            Inscriptions = new HashSet<Inscription>();
            Sessions = new HashSet<Session>();
            Subasta = new HashSet<Subastum>();
        }

        public string DocumentNumber { get; set; } = null!;
        public string TypeDocument { get; set; } = null!;
        public string NameUser { get; set; } = null!;
        public string SurnameUser { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Photo { get; set; }

        public virtual ICollection<Credential> Credentials { get; set; }
        public virtual ICollection<Inscription> Inscriptions { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Subastum> Subasta { get; set; }
    }
}
