using GestionHotel.Modeles;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Correction : utiliser le bon namespace EF Core


namespace GestionHotel.Controlleurs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly GestionHotelDbContext _context;
        public ClientController(GestionHotelDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère la liste de tous les clients 
        /// </summary>
        /// <returns code="200">La liste de tous les clients</returns>
        // GET: api/clients
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Client>>>ObtenirClients()
        {
            return await _context.Clients.ToListAsync();
        }
        // GET: api/clients/5
        /// <summary>
        /// Obtenir un client par son ID
        /// </summary>
        /// <param name="id"> L'Identifiant du client à récupérer</param>
        /// <returns>Le client correspondant</returns>
        /// <response code="200"> Client trouvé et retourné</response>
        /// <response code ="404">Client non trouvé</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> ObtenirClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return NotFound();
            return client;
        }

        /// <summary>
        /// Créér un nouveau client dans la base de données
        /// </summary>
        /// <param name="client">Les données du client à créer</param>
        /// <returns> Le client est créé </returns>
        /// <response code="201"> Le client est crée avec succès</response>
        /// <response code="400">Les données du client sont invalides</response>
        // POST: api/clients
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Client>> CreerClient(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenirClient), new { id = client.Id }, client);
        }

        /// <summary>
        /// Modifier les informations d'un client existant dans la base de données
        /// </summary>
        /// <param name="id"> L'identifiant du client à modifier</param>
        /// <param name="client">Les nouvelles données du client</param>
        /// <returns>Aucun contenu retourné (204)</returns>
        /// <response code="204">Client modifié avec succèes</response>
        /// <response code="400">L'identifiant du client ne correspond pas aux données fournies</response>
        /// <response code="404">Client non trouvé</response>
        // PUT: api/clients/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ModifierClient(int id, Client client)
        {
            if (id != client.Id) return BadRequest();
            _context.Entry(client).State = EntityState.Modified; // Utilise maintenant le bon EntityState de EF Core
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // DELETE: api/clients/5
        /// <summary>
        /// Supprime un client de la base de données en fonction de son identifiant
        /// </summary>
        /// <param name="id">L'identifiant du client</param>
        /// <returns>Aucun contenu retourné</returns>
        /// <response code="204">Le client a été suprimé avec succès</response>
        /// <response code="404">Client non trouvé</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> SupprimerClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return NotFound();
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
