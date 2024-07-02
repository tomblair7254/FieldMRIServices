using FieldMRIServices.Entities;

namespace FieldMRIServices.Model
{
    public class PhotoModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string InventoryName { get; set; }
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    }
}
