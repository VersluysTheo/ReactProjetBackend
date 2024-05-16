namespace reactBackend.Models
{
    public class Bien
    {
        public int BienId { get; set; }
        public string? Quartier { get; set; }
        public int? Prix { get; set; }
        public int? Nbr_Chambres { get; set; }
        public int? Nbr_Pieces { get; set; }
        public string? Classe_Energetique { get; set; }
        public long? Description { get; set; }
        public int? Surface_Habitable { get; set; }
        public int? Surface_Totale { get; set; }

        // Propriété de navigation vers l'utilisateur associé
        public ICollection<User> Users { get; set; }

    }
}
