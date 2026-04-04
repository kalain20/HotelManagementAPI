namespace GestionHotel.Modeles
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int ChambreId { get; set; }
        public Chambre Chambre { get; set; }
        public DateTime DateCheckin { get; set; }
        public DateTime DateCheckout { get; set; }
        public string Statut { get; set; } = "Confirmée"; // En cours, Terminée, Annulée
        public Facture? Facture { get; set; }
    }
}
