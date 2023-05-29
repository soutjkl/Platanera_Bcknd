
 using bckPlatanera.Data;
using bckPlatanera.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace bckPlatanera.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseofferController : ControllerBase
    {
        private readonly BdPlatContext _context;

        public PurchaseofferController(BdPlatContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Purchaseoffer> Get()
        {
            return _context.Purchaseoffers.ToList();
        }

    
    }
}