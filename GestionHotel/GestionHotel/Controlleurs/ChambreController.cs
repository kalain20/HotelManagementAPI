using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionHotel.Controlleurs
{
    using GestionHotel.Modeles;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class ChambresController : ControllerBase
    {
        private readonly GestionHotelDbContext _context;

        public ChambresController(GestionHotelDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Récupère la liste de toutes les chambres disponibles dans l'hôtel. 
        /// Chaque chambre est représentée par un objet contenant des informations telles que le numéro de chambre, 
        /// le type, le statut et le prix par nuit. 
        /// Cette méthode permet aux clients de consulter les options d'hébergement avant de faire une réservation.
        /// </summary>
        /// <returns code="200"></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Chambre>>> ObtenirChambres()
        {
            return await _context.Chambres.ToListAsync();
        }

        /// <summary>
        /// Obtient les détails d'une chambre spécifique en fonction de son identifiant.
        /// </summary>
        /// <param name="id"> L'identifiant de la chambre</param>
        /// <returns> La chambre correspondante</returns>
        /// <response code="200"> Chambre trouvée et retournée</response>
        /// <response code="400"> Chambre non trouvée</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
      
        public async Task<ActionResult<Chambre>> ObtenirChambre(int id)
        {
            var chambre = await _context.Chambres.FindAsync(id);
            if (chambre == null) return NotFound();
            return chambre;
        }
        /// <summary>
        /// Créer une nouvelle chambre dans la base de données.
        /// </summary>
        /// <param name="chambre">Les données de la chambre à créer</param>
        /// <returns>La chambre créeé</returns>
        /// <response code="200">La chambre est créé avec succès</response>
        /// <response code="400">Les données de la chambre sont invalides</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Chambre>> CreerChambre(Chambre chambre)
        {
            _context.Chambres.Add(chambre);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenirChambre), new { id = chambre.Id }, chambre);
        }
        /// <summary>
        /// Modifier les informations d'une chambre existante dans la base de données.
        /// </summary>
        /// <param name="id">L'identifiant de la chambre</param>
        /// <param name="chambre">Les données de la chambre àmodifier </param>
        /// <returns>Aucune contenu retouné (204)</returns>
        /// <response code="204">Chambre modifiée avec succès</response>
        /// <response code="400">L'identifiant de la chambre ne correspond pas aux données fournies</response>
        /// <response code="404"> Chambre non trouvée</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifierChambre(int id, Chambre chambre)
        {
            if (id != chambre.Id) return BadRequest();
            _context.Entry(chambre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Supprime une chambre existante dans la base de données.
        /// </summary>
        /// <param name="id">L'identifiant de la chambre</param>
        /// <returns>Aucun contenu retouné (204)</returns>
        /// <response code="204"> La chambre a été supprimée avec succès</response>
        /// <response code="404"> Chambre non trouvée</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChambre(int id)
        {
            var chambre = await _context.Chambres.FindAsync(id);
            if (chambre == null) return NotFound();

            _context.Chambres.Remove(chambre);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
