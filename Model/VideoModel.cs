using FieldMRIServices.Entities;

namespace FieldMRIServices.Model
{
    public class VideoModel
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
        public string GeneralImage { get; set; } = string.Empty;
        public string Location { get; set; }
        public string Tag { get; set; }
        public string Qty { get; set; }
        public string BarCodes { get; set; }
        public string Serial { get; set; }


        public List<VideoPhoto> VideoPhoto { get; set; }
    }
}
