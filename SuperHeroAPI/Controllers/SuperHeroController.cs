using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found.");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeros.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbhero = await _context.SuperHeros.FindAsync(request.Id);
            if (dbhero == null)
            {
                return BadRequest("Hero not found.");
            }

            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.LastName = request.LastName;
            dbhero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbhero = await _context.SuperHeros.FindAsync(id);
            if (dbhero == null)
            {
                return BadRequest("Hero not found.");
            }

            _context.SuperHeros.Remove(dbhero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }
    }
}
