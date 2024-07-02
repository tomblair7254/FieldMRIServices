using FieldMRIServices.Entities;

namespace FieldMRIServices.Model
{
    public class MemoryPhotoModel
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Image { get; set; }
        public int MemoryId { get; set; }
        public Memory Memory { get; set; }
    }
}
