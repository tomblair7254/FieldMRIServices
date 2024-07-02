namespace FieldMRIServices.Entities
{
    public class ComputerPhoto
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Image { get; set; }
        public int ComputerId { get; set; }
        public Computer Computer { get; set; }
    }
}
