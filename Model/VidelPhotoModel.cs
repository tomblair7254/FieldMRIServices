using FieldMRIServices.Entities;

namespace FieldMRIServices.Model
{
    public class VideoPhotoModel
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
        public string GeneralImage { get; set; } = string.Empty;
        public string Tag { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }
    }
}
