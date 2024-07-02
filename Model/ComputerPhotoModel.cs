using FieldMRIServices.Entities;

namespace FieldMRIServices.Model
{
    public class ComputerPhotoModel
    {
        public int Id { get; set; }
        public string InventoryName { get; set; }
        public string Image { get; set; }
        public int ComputerId { get; set; }
        public Computer Computer { get; set; }
    }
}
