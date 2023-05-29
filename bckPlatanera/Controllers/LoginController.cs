using bckPlatanera.Data;
using bckPlatanera.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace bckPlatanera.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BdPlatContext _context;
        private readonly IConfiguration _confi;

        public LoginController(BdPlatContext context, IConfiguration confi)
        {
            _context = context;
            _confi = confi;
        }

        [HttpPost]

        public async Task<IActionResult> Login([FromQuery] string userName, [FromQuery] string password)
        {
            string auxPassword = ToSHA256(password);
            Credential user = _context.Credentials.Where(c => c.Username == userName && c.Password == auxPassword).FirstOrDefault();
            Person perAux = _context.People.Where(p => p.DocumentNumber == user.NumberDocumentPerson).FirstOrDefault();
            if (user != null)
            {
                DateTime expirationDate = DateTime.UtcNow.AddMinutes(30);
                string token = generateToken(userName, expirationDate);
                return Ok(new { token = token, userId = user.NumberDocumentPerson, nameUser = perAux.NameUser, surname = perAux.SurnameUser, typeDocument = perAux.TypeDocument, address = perAux.Address, email = perAux.Email, photo = perAux.Photo, telephone = perAux.Telephone });
            }
            else
            {
                return BadRequest(new { message = "Credenciales incorrectas" });
            }
        }

        public static string ToSHA256(string s)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }




            [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(ProfileDto person)
        {
       
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                Credential crede= _context.Credentials.Where(p=> p.Username == person.Email).FirstOrDefault();
                
                if (crede == null)
                {
                Person personaux = _context.People.Where(a => a.DocumentNumber == person.DocumentNumber).FirstOrDefault();
                    if (personaux == null )
                    {

                        Person per1 = new Person() {
                            DocumentNumber = person.DocumentNumber,
                            TypeDocument = person.TypeDocument,
                            NameUser = person.NameUser,
                            SurnameUser = person.SurnameUser,
                            Address = person.Address,
                            Telephone = person.Telephone,
                            Email = person.Email
                        };

                        _context.People.Add(per1);
                      string passaux = ToSHA256(person.Password);
                             Credential cred = new Credential()
                            {
                                Username = person.Email,
                                Password = passaux,
                                NumberDocumentPerson = person.DocumentNumber
                            };
                            _context.Credentials.Add(cred);
                        await _context.SaveChangesAsync();
                   
                        return Created($"/User/{person.DocumentNumber}", person);
                    }
                    else {
                        return BadRequest(new { message = "Numero de documento repetido" });
                    }
                }
                else
                {
                        return BadRequest(new { message = "Username ya esta en uso" });
                }


            }
        }

        public string generateToken(string username, DateTime expirationDate)
        {
            
            var keyByte = Encoding.ASCII.GetBytes(_confi.GetSection("settings").GetSection("secretKey").ToString());
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims, 
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyByte), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(tokenConfig);
            return token;
        }

    }
}
