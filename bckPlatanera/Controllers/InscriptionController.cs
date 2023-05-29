using bckPlatanera.Data;
using bckPlatanera.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace bckPlatanera.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InscriptionController: ControllerBase
    {

        private readonly BdPlatContext _context;


        public InscriptionController(BdPlatContext context)
        {
            _context = context;

        }

        //Añade una nueva Inscripcion
        [Route("AddInscription")]
        [HttpPost]
        public async Task<IActionResult> AddInscription(PurchaXD purcha)
        {
            Inscription insAux = _context.Inscriptions.Where(c => c.SubastaIdSubasta == purcha.SubastaIdSubasta && c.PersonDocumentNumber==purcha.PersonDocumentNumber).FirstOrDefault();
            if (insAux == null)
            {
                Inscription auxInscription = new()
                {
                    DateInscription = DateTime.Now,
                    PersonDocumentNumber = purcha.PersonDocumentNumber,
                    SubastaIdSubasta = purcha.SubastaIdSubasta
                };
             Inscription ccc=   _context.Inscriptions.Add(auxInscription).Entity;
                await _context.SaveChangesAsync();
                if (purcha.PricePurchase == null)
                {
                    await _context.SaveChangesAsync();
                    return Created($"/Inscription/{auxInscription.IdInscription}", auxInscription);
                }
                else {

                    Purchaseoffer puchaAux = new Purchaseoffer()
                    {
                        InscriptionIdInscription = ccc.IdInscription,
                        DatePurchase = DateTime.Now,
                        PricePurchase = purcha.PricePurchase
                    };
                    _context.Purchaseoffers.Add(puchaAux);
                    await _context.SaveChangesAsync();
                    return Created($"/Purchaseoffer/{puchaAux.Idpurchaseoffer}", puchaAux);
   
                }
            } else if (insAux != null && purcha.PricePurchase != null) {
                Purchaseoffer purchaAux = _context.Purchaseoffers.Where(p => p.InscriptionIdInscription == insAux.IdInscription).FirstOrDefault();
                if (purchaAux == null)
                {
                    Purchaseoffer puchaAdd = new ()
                    {
                        PricePurchase = purcha.PricePurchase,
                        DatePurchase = DateTime.Now,
                        InscriptionIdInscription = insAux.IdInscription,
                    };
                    _context.Purchaseoffers.Add(puchaAdd);
                    await _context.SaveChangesAsync();
                    
                   return Created($"/Purchaseoffer/{puchaAdd.Idpurchaseoffer}", puchaAdd); 
                 
                }
                else
                {
                    purchaAux.DatePurchase = DateTime.Now;
                    purchaAux.PricePurchase = purcha.PricePurchase;
                    await _context.SaveChangesAsync();
                    return Ok(purchaAux);
                }
            }
            else {
                return BadRequest(new { message = "Algo anda mal xD" });
            }
        }


   


        [HttpGet]
        [Route("GetInscripcionesId")]
        public  IEnumerable<Compras> GetInscripcionesId(string idPerson)
        {
            var inscri= _context.Inscriptions.Where(x => x.PersonDocumentNumber == idPerson).ToList();
            var listSubastas = new List<Compras>();
            foreach (var item in inscri) {
                 var auxvar =_context.Subasta.Where(x => x.IdSubasta == item.SubastaIdSubasta).FirstOrDefault();
                var auxPurcha = _context.Purchaseoffers.Where(p => p.InscriptionIdInscription == item.IdInscription).FirstOrDefault();
                listSubastas.Add(new Compras()
                {
                    IdSubasta = auxvar.IdSubasta,
                    BananaType= auxvar.BananaType,
                    DateEnded=auxvar.DateEnded,
                    MeasurementUnits=auxvar.MeasurementUnits,
                    DateStarted=auxvar.DateStarted,
                    PricePurchase=auxPurcha.PricePurchase

                });
                
            }
            return listSubastas;

        }
        //Verifica si la persona esta inscrita ala Subastaa 
        [Route("isIncritoSubasta")]
        [HttpPost]
        public async Task<IActionResult> isIncritoSubasta(PurchaXD purchssaxD) {
            Inscription isIns = _context.Inscriptions.Where(i => i.SubastaIdSubasta == purchssaxD.SubastaIdSubasta && i.PersonDocumentNumber == purchssaxD.PersonDocumentNumber).FirstOrDefault();
            if (isIns == null)
            {
                return Ok("false");
            }
            else { 
                return Ok("true");
            }
        }
        //elimina una Inscripcion a Una subasta
        [HttpDelete]
        [Route("remove")]
        public async Task<IActionResult> remove(string idPerson , int idSubasta)
        {
            Inscription insciAux= _context.Inscriptions.Where(i=>i.SubastaIdSubasta==idSubasta && i.PersonDocumentNumber == idPerson).FirstOrDefault();
            List<Purchaseoffer>purcheaux=_context.Purchaseoffers.Where(p=>p.InscriptionIdInscription== insciAux.IdInscription).ToList();
            if (purcheaux == null)
            {
                return StatusCode(404);
            }
            else {
                foreach (var item in purcheaux)
                {
                    _context.Purchaseoffers.Remove(item);
                }
                await _context.SaveChangesAsync();
            }
            if (insciAux == null)
            {
                return StatusCode(404);
            }
            _context.Inscriptions.Remove(insciAux);
            await _context.SaveChangesAsync();
            return Ok(insciAux);
        }
    }
}
