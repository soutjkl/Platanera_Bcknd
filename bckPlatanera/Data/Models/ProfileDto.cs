namespace bckPlatanera.Data.Models
{
    public class ProfileDto
    {
        public string DocumentNumber { get; set; } = null!;

        public string TypeDocument { get; set; } = null!;

        public string NameUser { get; set; } = null!;

        public string SurnameUser { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Telephone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Photo { get; set; }
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
