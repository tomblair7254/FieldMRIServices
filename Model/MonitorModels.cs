using FieldMRIServices.Entities;

namespace FieldMRIServices.Model
{
    public class MonitorModels
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string Status { get; set; }
        public string ComputerName { get; set; }
        public string GeneralImage { get; set; }
        public string Manufactor { get; set; }
        public string Size { get; set; }
        public string Resolution { get; set; }
        public string Location { get; set; }
        public string Tag { get; set; }
        public string Qty { get; set; }
        public string BarCodes { get; set; }

        public List<MonitorPhoto> MonitorPhoto { get; set; }
    }
}
