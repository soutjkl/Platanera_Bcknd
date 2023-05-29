
 using Microsoft.AspNetCore.Mvc;
using bckPlatanera.Data;
using bckPlatanera.Data.Models;

namespace bckPlatanera.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {

        private readonly BdPlatContext _context;

        public DepartmentController(BdPlatContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Department> Get()
        {
            return _context.Departments.ToList();
        }
        [HttpGet]
        [Route("GetCities")]
        public IEnumerable<City> GetCities(int idDepartment) {
         return _context.Cities.Where(x => x.DepartmentsIdDepartments ==idDepartment).ToList();
             
          
            
        }
           
       
    }
}
