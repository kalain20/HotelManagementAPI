namespace GestionHotel.Modeles
{
    public class Chambre
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Type { get; set; } = "Standard"; // ex: Standard, Prestige, Suite
        public string Statut { get; set; } = "Libre";  // Libre, Occupée, Maintenance
        public decimal PrixParNuit { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}
