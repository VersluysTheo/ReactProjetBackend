namespace reactBackend.Models
{
    public class Bien
    {
        public int BienId { get; set; }
        public string? Quartier { get; set;}
        public int? Prix { get; set;}
        public int? NbrChambres {  get; set;}
        public int? Nbr_Pieces {  get; set;}
        public string? ClasseEnergetique { get; set;}
        public long? Description { get; set; }
        public int? SurfaceHabitable { get; set; }
        public int? SurfaceTotale { get;set; }

        public string? TestPhotos { get; set; } // Test pour ajouter les images du bien

    }
}
