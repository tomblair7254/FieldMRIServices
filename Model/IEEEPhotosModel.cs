using FieldMRIServices.Entities;

namespace FieldMRIServices.Model
{
    public class IEEEPhotosModel
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Image { get; set; }
        public int IEEEId { get; set; }
        public IEEE IEEE { get; set; }
    }
}
