using FieldMRIServices.Entities;

namespace FieldMRIServices.Model
{
    public class MonitorPhotoModel
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Image { get; set; }
        public int MonitorId { get; set; }
        public Monitors Monitors { get; set; }
    }
}
