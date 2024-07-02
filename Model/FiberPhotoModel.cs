using FieldMRIServices.Entities;

namespace FieldMRIServices.Model
{
    public class FiberPhotoModel
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Image { get; set; }
        public int FiberId { get; set; }
        public Fiber Fiber { get; set; }
    }
}
