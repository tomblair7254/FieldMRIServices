using FieldMRIServices.Model;

namespace FieldMRIServices.Entities
{
    public class MosdiskPhoto
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Serial { get; set; }
        public string Status { get; set; }
        public string GeneralImage { get; set; } = string.Empty;


        public int modiskId { get; set; }
        public Modisk Modisk { get; set; }
    }
}
