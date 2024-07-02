using FieldMRIServices.Entities;

namespace FieldMRIServices.Model
{
    public class HarddrivePhotoModel
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string InventoryName { get; set; }
        public string Image { get; set; }
        public int HarddriveId { get; set; }
        public Harddrive Harddrive { get; set; }
    }
}
