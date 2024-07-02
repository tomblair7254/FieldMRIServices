namespace FieldMRIServices.Entities
{
    public class VideoPhoto
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
        public string GeneralImage { get; set; } = string.Empty;
        public int VideoId { get; set; }
        public Video Video { get; set; }
    }
}