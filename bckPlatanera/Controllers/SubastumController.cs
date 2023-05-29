
 using Microsoft.AspNetCore.Mvc;
using bckPlatanera.Data;
using bckPlatanera.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Data;

namespace bckPlatanera.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubastumController : ControllerBase
    {

        private readonly BdPlatContext _context;

        public SubastumController(BdPlatContext context)
        {
            _context = context;
        }

        //muestra las subastas del sistema
        [HttpGet]
        public IEnumerable<Publication> Get()
        {
            var listPublications = new List<Publication>();
            var listSubastas = _context.Subasta.ToList();
            foreach (var subasta in listSubastas)
            {
                var personAux = _context.People.Where(p => p.DocumentNumber == subasta.PersonDocumentNumber).FirstOrDefault();
                var cityAux = _context.CityHasSubasta.Where(c => c.SubastaIdSubasta == subasta.IdSubasta).FirstOrDefault();
                var namecity = _context.Cities.Where(nc => nc.IdCity == cityAux.CityIdCity).FirstOrDefault();
                var departaux = _context.Departments.Where(d => d.IdDepartments == namecity.DepartmentsIdDepartments).FirstOrDefault();
                var compar = subasta.DateStarted.CompareTo(DateTime.Now);
                var runAux = (compar <= 0) ? true : false;
                compar = subasta.DateEnded.CompareTo(DateTime.Now);
                var staAux = (compar < 0) ? false : true;
                listPublications.Add(new Publication()
                {
                    IdSubasta = subasta.IdSubasta,
                    DateStarted = subasta.DateStarted,
                    DateEnded = subasta.DateEnded,
                    InitialPrice = subasta.InitialPrice,
                    Photos = subasta.Photos,
                    BananaType = subasta.BananaType,
                    DescriptionProduct = subasta.DescriptionProduct,
                    MeasurementUnits = subasta.MeasurementUnits,
                    PersonDocumentNumber = subasta.PersonDocumentNumber,
                    NamePerson = personAux.NameUser,
                    NameCity = namecity.NameCity,
                    NameDepartment = departaux.NameDepartments,
                    Run = runAux,
                    Status = staAux
                });
            }
            return listPublications;
        }

        //muestra las subastas de la cuenta
        [Route("GetPorId")]
        [HttpGet]
        public IEnumerable<Ventas> GetPorId(string personDocument)
        {
            var listPublications = new List<Ventas>();
            var listSubastas = _context.Subasta.Where(s => s.PersonDocumentNumber == personDocument).ToList();
            foreach (var subasta in listSubastas)
            {

                listPublications.Add(new Ventas()
                {
                    IdSubasta = subasta.IdSubasta,
                    DateStarted = subasta.DateStarted,
                    DateEnded = subasta.DateEnded,
                    BananaType = subasta.BananaType,
                    InitialPrice = subasta.InitialPrice,
                    MeasurementUnits = subasta.MeasurementUnits
                });
            }
            return listPublications;
        }

        //obtiene las subastas por precio inicial
        [HttpGet]
        [Route("GetByPrice")]
        public ActionResult<Subastum> GetByPrice(String price)
        {
            try
            {
                var user = _context.Subasta.ToList();
                if (price != null)
                {
                    user = user.Where(x => x.InitialPrice.ToString().IndexOf(price) > -1).ToList();
                }
                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetByProduct")]
        public ActionResult<Person> GetByProduct(String nameUser)
        {
            try
            {
                var user = _context.Subasta.ToList();
                if (nameUser != null)
                {
                    user = user.Where(x => x.BananaType.ToLower().IndexOf(nameUser) > -1).ToList();
                }
                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
        }

        //añade una nueva subasta
        [Route("AddSubasta")]
        [HttpPost]
        public async Task<IActionResult> AddSubasta(SubastaXD subasta)
        {


            Subastum subaux = new()
            {
                DateStarted = subasta.DateStarted,
                DateEnded = subasta.DateEnded,
                BananaType = subasta.BananaType,
                InitialPrice = subasta.InitialPrice,
                Photos = subasta.Photos,
                DescriptionProduct = subasta.DescriptionProduct,
                MeasurementUnits = subasta.MeasurementUnits,
                PersonDocumentNumber = subasta.PersonDocumentNumber,
                CityHasSubasta = null,
                PersonDocumentNumberNavigation = null,
                Inscriptions = null

            };
            Subastum subastaaAux = _context.Subasta.Add(subaux).Entity;
            await _context.SaveChangesAsync();
            City cityss = _context.Cities.Where(c => c.IdCity == subasta.CityIdCity).FirstOrDefault();
            CityHasSubastum cyhassu = new()
            {
                CityIdCity = cityss.IdCity,
                SubastaIdSubasta = subastaaAux.IdSubasta,
                SubastaIdSubastaNavigation = null
            };
            _context.CityHasSubasta.Add(cyhassu);
            await _context.SaveChangesAsync();
            return Created($"/Subastum/{subaux.IdSubasta}", subaux);

        }

        //elimina una publicacion de subasta
        [HttpDelete]
        [Route("remove")]
        public async Task<IActionResult> remove(int id)
        {
            Subastum subas = _context.Subasta.Where(s => s.IdSubasta == id).FirstOrDefault();
            if (subas != null)
            {
                List<Inscription> auxList = _context.Inscriptions.Where(i => i.SubastaIdSubasta == subas.IdSubasta).ToList();
                if (auxList.Count > 0)
                {
                    foreach (var item in auxList)
                    {
                        List<Purchaseoffer> purcheaux = _context.Purchaseoffers.Where(p => p.InscriptionIdInscription == item.IdInscription).ToList();
                        if (purcheaux == null)
                        {
                            return StatusCode(404);
                        }
                        else
                        {
                            foreach (var xD in purcheaux)
                            {
                                _context.Purchaseoffers.Remove(xD);
                            }
                            await _context.SaveChangesAsync();
                        }

                        _context.Inscriptions.Remove(item);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            CityHasSubastum cityHas = _context.CityHasSubasta.Where(c => c.SubastaIdSubasta == subas.IdSubasta).FirstOrDefault();
            _context.CityHasSubasta.Remove(cityHas);
            await _context.SaveChangesAsync();
            if (subas == null)
            {
                return StatusCode(404);
            }
           
            _context.Subasta.Remove(subas);
            await _context.SaveChangesAsync();
            return Ok(subas);
        }


        //Lista de Pujas por Subasta
        [Route("totalPurchase")]
        [HttpGet]
        public  IEnumerable<PurchaSS> totalPurchase(int idSubastax)
        {

            List<Inscription> auxList = _context.Inscriptions.Where(i => i.SubastaIdSubasta == idSubastax).ToList();
            List<PurchaSS> listPurshe=new List<PurchaSS>();
            foreach(Inscription item in auxList)
            {
                Purchaseoffer aux = _context.Purchaseoffers.Where(p => p.InscriptionIdInscription == item.IdInscription).FirstOrDefault();
                Person auxPerson=_context.People.Where(p => p.DocumentNumber==item.PersonDocumentNumber).FirstOrDefault();
                listPurshe.Add(new ()
                {
                    PersonDocumentNumber = item.PersonDocumentNumber,
                    PricePurchase = aux.PricePurchase,
                    DatePurchase = aux.DatePurchase,
                    NamePerson= auxPerson.NameUser + " "+auxPerson.SurnameUser,
                    SubastaIdSubasta = item.SubastaIdSubasta
                }) ;
            
            }

            return listPurshe.OrderByDescending(x => x.PricePurchase); ;
        }

    }
}

 