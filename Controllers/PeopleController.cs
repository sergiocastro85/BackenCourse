using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet("all")]
        public List<People> GetAll() => Repository.People;


        //[HttpGet("{id}")]
        //public People Get(int id) => Repository.People.First(p => p.Id == id);


        [HttpGet("search/{search}")]
        public List<People> Get(string search) => 
            Repository.People.Where (p=>p.Name.ToUpper().Contains(search.ToUpper())).ToList();


        [HttpGet("{id}")]
        public ActionResult<People> GetId(int id)
        {
           var people=Repository.People.FirstOrDefault(p => p.Id == id);

            if (people == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(people);
            }
        }

        [HttpPost]
        public IActionResult Add(People people) 
        {
            if (string.IsNullOrEmpty(people.Name)) 
            {
                return BadRequest();
            }

            Repository.People.Add(people);

            return NoContent();
        }

    }

    public class Repository()
    {
        public static List<People> People = new List<People>
        {
            new People()
            {
                Id = 1,
                Name="Sergio",
                Birthdate = DateTime.Now,
            },

            new People()
            {
                Id = 2,
                Name="kevin",
                Birthdate = DateTime.Now,
            },


            new People()
            {
                Id = 3,
                Name="Mariadelmar",
                Birthdate = DateTime.Now,
            },
        };
    }

    public class People ()
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
