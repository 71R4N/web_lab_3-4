using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        CarContext db;
        public CarsController(CarContext context)
        {
            db = context;
            if (!db.Car.Any())
            {
                db.Car.Add(new Models.Car { firm = "Firm1", model = "Model1", year = 2022, power = 111, color = "black", price = 1000 });
                db.Car.Add(new Models.Car { firm = "Firm2", model = "Model2", year = 2020, power = 222, color = "white", price = 2000 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Car>>> Get()
        {
            return await db.Car.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Car>> Get(int id)
        {
            Models.Car user = await db.Car.FirstOrDefaultAsync(x => x.car_id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Models.Car>> Post(Models.Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }

            db.Car.Add(car);
            await db.SaveChangesAsync();
            return Ok(car);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Models.Car>> Put(Models.Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }
            if (!db.Car.Any(x => x.car_id == car.car_id))
            {
                return NotFound();
            }

            db.Update(car);
            await db.SaveChangesAsync();
            return Ok(car);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Car>> Delete(int id)
        {
            Models.Car car = db.Car.FirstOrDefault(x => x.car_id == id);
            if (car == null)
            {
                return NotFound();
            }
            db.Car.Remove(car);
            await db.SaveChangesAsync();
            return Ok(car);
        }
    }
}
