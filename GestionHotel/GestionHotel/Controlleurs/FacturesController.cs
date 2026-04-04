using GestionHotel.Modeles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionHotel.Controlleurs
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturesController : ControllerBase
    {
        private readonly GestionHotelDbContext _context;
        public FacturesController(GestionHotelDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facture>>> GetFactures()
        {
            return await _context.Factures
                .Include(f => f.Reservation)
                .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Facture>> GetFacture(int id)
        {
            var facture = await _context.Factures
                .Include(f => f.Reservation)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (facture == null) return NotFound();
            return facture;
        }
        [HttpPost]
        public async Task<ActionResult<Facture>> PostFacture(Facture facture)
        {
            _context.Factures.Add(facture);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFacture), new { id = facture.Id }, facture);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacture(int id, Facture facture)
        {
            if (id != facture.Id) return BadRequest();
            _context.Entry(facture).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacture(int id)
        {
            var facture = await _context.Factures.FindAsync(id);
            if (facture == null) return NotFound();
            _context.Factures.Remove(facture);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
