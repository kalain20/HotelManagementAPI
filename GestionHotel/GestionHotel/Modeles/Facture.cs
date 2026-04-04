namespace GestionHotel.Modeles
{
    public class Facture
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public decimal MontantTotal { get; set; }
        public DateTime DateFacture { get; set; } = DateTime.Now;
        public string ModePaiement { get; set; } = "Carte";
    }
}
