namespace FieldMRIServices.Entities
{
    public class FiberPhoto
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Image { get; set; }
        public int FiberId { get; set; }
        public Fiber Fiber { get; set; }
    }
}
