namespace FieldMRIServices.Entities
{
    public class DVDPhoto
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Image { get; set; }
        public int DVDId { get; set; }
        public DVD DVD { get; set; }
    }
}
