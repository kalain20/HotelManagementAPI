using Microsoft.EntityFrameworkCore;

namespace GestionHotel.Modeles
{
    public class GestionHotelDbContext : DbContext
    {
        public GestionHotelDbContext(DbContextOptions<GestionHotelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Chambre> Chambres { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Facture> Factures { get; set; } = null!;

       
    }
}